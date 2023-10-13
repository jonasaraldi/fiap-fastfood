using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Contracts.Abstractions;

namespace FastFood.Atendimento.Domain.Pedidos.Entities;

public class ItemDePedido : AuditableEntity
{
    private ItemDePedido()
    {
    }
    
    private ItemDePedido(string nome, string descricao, Dinheiro preco, int quantidade, string? observacao)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidade;
        Observacao = observacao;
    }

    public Pedido Pedido { get; private set; }
    public Ulid PedidoId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Dinheiro Preco { get; private set; }
    public int Quantidade { get; private set; }
    public string? Observacao { get; private set; }

    public void SetPedido(Pedido pedido)
    {
        Pedido = pedido;
        PedidoId = pedido.Id;
    }
    
    public static ItemDePedido Criar(string nome, string descricao, Dinheiro preco, int quantidade, string? observacao = null) => 
        new(nome, descricao, preco, quantidade, observacao);
}