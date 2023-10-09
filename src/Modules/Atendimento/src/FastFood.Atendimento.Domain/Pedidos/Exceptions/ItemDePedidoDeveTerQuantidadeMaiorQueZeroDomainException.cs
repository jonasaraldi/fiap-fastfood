using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class ItemDePedidoDeveTerQuantidadeMaiorQueZeroDomainException : InvalidOperationDomainException
{
    public ItemDePedidoDeveTerQuantidadeMaiorQueZeroDomainException() 
        : base("Item de pedido deve ter quantidade maior que zero.")
    {
    }
}