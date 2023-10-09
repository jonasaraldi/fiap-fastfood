using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException
    : DomainException 
{
    public ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException(StatusDePedido status) 
        : base($"Item de pedido não pode ser adicionado em pedido com status {status.Descricao.ToLower()}")
    {
    }
}