using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException
    : DomainException 
{
    public CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException(StatusDePedido status) 
        : base($"CPF não pode ser alterado em pedido com status {status.Descricao.ToLower()}.")
    {
    }
}