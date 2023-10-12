using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class PedidoNaoPodeSerConfirmadoSemItensDomainException : InvalidOperationDomainException
{
    public PedidoNaoPodeSerConfirmadoSemItensDomainException() 
        : base("Pedido não pode ser confirmado sem itens.")
    {
    }
}