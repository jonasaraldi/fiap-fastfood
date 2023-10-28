using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs.Converters;

public class StatusDePedidoToStringConverter : ValueConverter<StatusDePedido, string>
{
    public StatusDePedidoToStringConverter()
        : base(
            convertToProviderExpression: statusDePedido => statusDePedido.Codigo,
            convertFromProviderExpression: codigo => GetByCodigo(codigo))
    {   
    }
    
    private static StatusDePedido GetByCodigo(string codigo) =>
        codigo switch
        {
            nameof(PedidoPendente) => new PedidoPendente(),
            nameof(PedidoCancelado) => new PedidoCancelado(),
            nameof(PedidoConfirmado) => new PedidoConfirmado(),
            nameof(PedidoRecebido) => new PedidoRecebido(),
            nameof(PedidoEmPreparacao) => new PedidoEmPreparacao(),
            nameof(PedidoPronto) => new PedidoPronto(),
            nameof(PedidoFinalizado) => new PedidoFinalizado(),
            _ => throw new StatusDePedidoNaoEncontradoDomainException(codigo)
        };
}