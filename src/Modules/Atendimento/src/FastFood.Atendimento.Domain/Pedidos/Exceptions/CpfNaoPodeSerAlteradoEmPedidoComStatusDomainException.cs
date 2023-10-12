using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException
    : DomainException 
{
    public CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException(StatusDePedido status) 
        : base($"CPF não pode ser alterado em pedido com status {status.Descricao.ToLower()}")
    {
    }
}