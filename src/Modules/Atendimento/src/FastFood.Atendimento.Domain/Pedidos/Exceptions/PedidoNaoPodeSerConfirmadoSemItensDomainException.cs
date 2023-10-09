using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class PedidoNaoPodeSerConfirmadoSemItensDomainException : InvalidOperationDomainException
{
    public PedidoNaoPodeSerConfirmadoSemItensDomainException() 
        : base("Pedido não pode ser confirmado sem itens.")
    {
    }
}