using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException
    : DomainException 
{
    public ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException(StatusDePedido status) 
        : base($"Item de pedido não pode ser removido em pedido com status {status.Descricao.ToLower()}.")
    {
    }
}