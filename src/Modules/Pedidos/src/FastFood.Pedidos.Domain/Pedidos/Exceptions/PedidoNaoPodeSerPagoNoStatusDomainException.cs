using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class PedidoNaoPodeSerPagoNoStatusDomainException
    : DomainException 
{
    public PedidoNaoPodeSerPagoNoStatusDomainException(StatusDePedido status) 
        : base($"Pedido não pode ser pago no status {status.Descricao.ToLower()}.")
    {
    }
}