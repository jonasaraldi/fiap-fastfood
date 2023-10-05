using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Entities;

public sealed class Cliente : Entity
{
    private Cliente()
    {
    }

    private Cliente(string nome, Email email)
    {
        Nome = nome;
        Email = email;
    }
    
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public ICollection<Pedido> Pedidos { get; set; }
    
    public static Cliente Criar(string nome, Email email) => 
        new (nome, email);
}