using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Converters;

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
            nameof(PedidoCriado) => new PedidoCriado(),
            nameof(PedidoCancelado) => new PedidoCancelado(),
            nameof(PedidoConfirmado) => new PedidoConfirmado(),
            nameof(PedidoRecebido) => new PedidoRecebido(),
            nameof(PedidoEmPreparacao) => new PedidoEmPreparacao(),
            nameof(PedidoPronto) => new PedidoPronto(),
            nameof(PedidoFinalizado) => new PedidoFinalizado(),
            _ => throw new StatusDePedidoNaoEncontradoDomainException(codigo)
        };
}