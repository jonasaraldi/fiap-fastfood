using FastFood.Contracts.Abstractions;
using FastFood.Contracts.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos;

public sealed class Pedido : AggregateRoot
{
    private List<ItemDePedido> _itens = new();
    private List<HistoricoDePedido> _historicos = new();

    private Pedido()
    {
    }
    
    private Pedido(IEnumerable<ItemDePedido> itens)
    {
        Codigo = GerarCodigo();
        SetStatus(new PedidoPendente());
        AdicionarItens(itens);
        
        RaiseDomainEvent(new DomainEvents.PedidoCriado(Id));
    }

    public string Codigo { get; private set; }
    public StatusDePedido Status { get; private set; }
    public IReadOnlyCollection<ItemDePedido> Itens => _itens.ToList();
    public IReadOnlyCollection<HistoricoDePedido> Historicos => _historicos.ToList();
    public Cliente? Cliente { get; private set; }
    public Ulid? ClienteId { get; private set; }
    public Cpf? Cpf { get; private set; }
    public decimal ValorTotal => _itens.Sum(item => item.Quantidade * item.Preco);
    public bool PossuiItens => _itens.Any();
    public bool Pago { get; private set; }

    private string GerarCodigo()
    {
        return Guid.NewGuid()
            .ToString()
            .Substring(0, 5)
            .ToUpper();
    }

    public Pedido AdicionarItens(IEnumerable<ItemDePedido> itens)
    {
        foreach (var item in itens)
            AdicionarItem(item);

        return this;
    }

    public Pedido AdicionarItem(ItemDePedido item)
    {
        if (Status is not PedidoPendente)
            throw new ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException(Status);
        
        item.SetPedido(this);
        
        _itens.Add(item);
        RaiseDomainEvent(new DomainEvents.ItemDePedidoAdicionado(Id, item.Id));
        
        return this;
    }

    public Pedido RemoverItem(Ulid itemDePedidoId)
    {
        if (Status is not PedidoPendente)
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
        Pago = true;
        RaiseDomainEvent(new DomainEvents.PedidoRecebido(Id));
        
        return this;
    }
    
    public Pedido Preparar()
    {
        Status.Preparar(this);
        RaiseDomainEvent(new DomainEvents.PedidoEmPreparacao(Id));
        
        return this;
    }
    
    public Pedido MarcarComoPronto()
    {
        Status.MarcarComoPronto(this);
        RaiseDomainEvent(new DomainEvents.PedidoPronto(Id));
        
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
        if (Status is not PedidoPendente)
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

    public static Pedido Criar() => 
        new(Array.Empty<ItemDePedido>());
}