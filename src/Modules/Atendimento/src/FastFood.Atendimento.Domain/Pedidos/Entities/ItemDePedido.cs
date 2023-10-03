using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Entities;

public class ItemDePedido : Entity
{
    public ItemDePedido(string nome, string descricao, Dinheiro preco, int quantidade)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidade;
    }
    
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Dinheiro Preco { get; private set; }
    public int Quantidade { get; private set; }

    public static ItemDePedido Criar(string nome, string descricao, Dinheiro preco, int quantidade)
    {
        return new(nome, descricao, preco, quantidade);
    }
}