using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class TrocaDeStatusInvalidaDomainException : DomainException
{
    public TrocaDeStatusInvalidaDomainException(StatusDePedido atual, StatusDePedido informado) 
        : base($"Pedido não pode ser alterado de { atual.Descricao.ToLower() } para { informado.Descricao.ToLower() }.")
    {
    }
}