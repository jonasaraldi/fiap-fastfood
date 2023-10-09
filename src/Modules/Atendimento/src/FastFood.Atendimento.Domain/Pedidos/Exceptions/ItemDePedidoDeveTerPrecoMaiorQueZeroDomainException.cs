using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public class ItemDePedidoDeveTerPrecoMaiorQueZeroDomainException : InvalidOperationDomainException
{
    public ItemDePedidoDeveTerPrecoMaiorQueZeroDomainException() 
        : base("Item de pedido deve ter preço maior que zero.")
    {
    }
}