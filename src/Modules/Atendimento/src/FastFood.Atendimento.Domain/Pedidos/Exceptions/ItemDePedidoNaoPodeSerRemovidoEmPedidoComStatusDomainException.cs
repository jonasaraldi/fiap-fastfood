using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException
    : DomainException 
{
    public ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException(StatusDePedido status) 
        : base($"Item de pedido não pode ser removido em pedido com status {status.Descricao.ToLower()}")
    {
    }
}