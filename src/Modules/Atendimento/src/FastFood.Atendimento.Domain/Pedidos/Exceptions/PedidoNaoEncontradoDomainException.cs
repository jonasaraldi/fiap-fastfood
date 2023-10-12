using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class PedidoNaoEncontradoDomainException : NotFoundDomainException
{
    public PedidoNaoEncontradoDomainException() 
        : base("Pedido não encontrado.")
    {
    }
}