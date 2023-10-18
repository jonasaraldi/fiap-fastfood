using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Contracts.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.Contracts.Abstractions;

namespace FastFood.Atendimento.Domain.Pedidos;

public sealed class Pedido : AggregateRoot
{
    private List<ItemDePedido> _itens = new();
    private List<HistoricoDePedido> _historicos = new();

    private Pedido()
    {
        Codigo = GerarCodigo();
        SetStatus(new PedidoCriado());
        
        RaiseDomainEvent(new DomainEvents.PedidoCriado(Id));
    }

    public string Codigo { get; private set; }
    public StatusDePedido Status { get; private set; }
    public IReadOnlyCollection<ItemDePedido> Itens => _itens.ToList();
    public IReadOnlyCollection<HistoricoDePedido> Historicos => _historicos.ToList();
    public Cliente? Cliente { get; private set; }
    public Ulid? ClienteId { get; set; }
    public Cpf? Cpf { get; private set; }
    public decimal ValorTotal => _itens.Sum(item => item.Quantidade * item.Preco);
    public bool PossuiItens => _itens.Any();

    private string GerarCodigo()
    {
        return Guid.NewGuid()
            .ToString()
            .Substring(0, 5)
            .ToUpper();
    }

    public Pedido AdicionarItem(ItemDePedido item)
    {
        if (Status is not PedidoCriado)
            throw new ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException(Status);
        
        item.SetPedido(this);
        
        _itens.Add(item);
        RaiseDomainEvent(new DomainEvents.ItemDePedidoAdicionado(Id, item.Id));
        
        return this;
    }

    public Pedido RemoverItem(Ulid itemDePedidoId)
    {
        if (Status is not PedidoCriado)
            throw new ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException(Status);
        
        var item = _itens.FirstOrDefault(item => item.Id.Equals(itemDePedidoId));
        if (item is null) return this;
        
        _itens.Remove(item);
        RaiseDomainEvent(new DomainEvents.ItemDePedidoRemovido(Id, item.Id));

        return this;
    }

    public Pedido Cancelar()
    {
        Status.Cancelar(this);
        RaiseDomainEvent(new DomainEvents.PedidoCancelado(Id));
        
        return this;
    }
    
    public Pedido Confirmar()
    {
        Status.Confirmar(this);
        RaiseDomainEvent(new DomainEvents.PedidoConfirmado(Id));
        
        return this;
    }
    
    public Pedido Receber()
    {
        Status.Receber(this);
        return this;
    }
    
    public Pedido Preparar()
    {
        Status.Preparar(this);
        return this;
    }
    
    public Pedido MarcarComoPronto()
    {
        Status.MarcarComoPronto(this);
        return this;
    }
    
    public Pedido Finalizar()
    {
        Status.Finalizar(this);
        RaiseDomainEvent(new DomainEvents.PedidoFinalizado(Id));
        
        return this;
    }

    public Pedido SetCliente(Cliente cliente)
    {
        Cliente = cliente;
        ClienteId = cliente.Id;
        RaiseDomainEvent(new DomainEvents.ClienteIdentificado(
            Id, cliente.Id, cliente.Nome, cliente.Email));
        
        return this;
    }
    
    public Pedido SetCpf(Cpf cpf)
    {
        if (Status is not PedidoCriado)
            throw new CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException(Status);
        
        Cpf = cpf;
        return this;
    }
    
    internal void SetStatus(StatusDePedido status)
    {
        Status = status;
        RegisterUpdate();
        RegistrarHistorico();
    }

    private void RegistrarHistorico() => 
        _historicos.Add(HistoricoDePedido.Criar(this));

    public static Pedido Criar() => new();
}