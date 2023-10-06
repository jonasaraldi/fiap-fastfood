using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class StatusDePedidoNaoEncontradoDomainException : DomainException
{
    public StatusDePedidoNaoEncontradoDomainException(string codigo) 
        : base($"Status de pedido não encontrado ({ codigo }).")
    {
    }
}