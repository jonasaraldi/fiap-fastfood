using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class StatusDePedidoNaoEncontradoDomainException : DomainException
{
    public StatusDePedidoNaoEncontradoDomainException(string codigo) 
        : base($"Status de pedido não encontrado ({ codigo }).")
    {
    }
}