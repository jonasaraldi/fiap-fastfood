using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Entities;

public class ItemDePedido : AuditableEntity
{
    private ItemDePedido()
    {
    }
    
    private ItemDePedido(string nome, string descricao, Dinheiro preco, int quantidade)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidade;
    }

    public Pedido Pedido { get; private set; }
    public Ulid PedidoId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Dinheiro Preco { get; private set; }
    public int Quantidade { get; private set; }

    public void SetPedido(Pedido pedido)
    {
        Pedido = pedido;
        PedidoId = pedido.Id;
    }
    
    public static ItemDePedido Criar(string nome, string descricao, Dinheiro preco, int quantidade) => 
        new(nome, descricao, preco, quantidade);
}