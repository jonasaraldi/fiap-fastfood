using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException
    : DomainException 
{
    public ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException(StatusDePedido status) 
        : base($"Item de pedido não pode ser adicionado em pedido com status {status.Descricao.ToLower()}.")
    {
    }
}