using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Converters;

public class StatusDePedidoToStringConverter : ValueConverter<StatusDePedido, string>
{
    public StatusDePedidoToStringConverter()
        : base(
            convertToProviderExpression: statusDePedido => statusDePedido.Codigo,
            convertFromProviderExpression: codigo => StatusDePedido.GetByCodigo(codigo))
    {   
    }
}