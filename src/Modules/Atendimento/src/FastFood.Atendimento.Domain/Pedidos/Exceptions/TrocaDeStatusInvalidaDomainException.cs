using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class TrocaDeStatusInvalidaDomainException : DomainException
{
    public TrocaDeStatusInvalidaDomainException(StatusDePedido atual, StatusDePedido informado) 
        : base($"Pedido não pode ser alterado de { atual.Descricao.ToLower() } para { informado.Descricao.ToLower() }.")
    {
    }
}