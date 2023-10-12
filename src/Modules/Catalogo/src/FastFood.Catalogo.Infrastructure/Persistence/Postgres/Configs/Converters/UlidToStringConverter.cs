using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFood.Catalogo.Infrastructure.Persistence.Postgres.Configs.Converters;

internal class UlidToStringConverter : ValueConverter<Ulid, string>
{
    public UlidToStringConverter()
        : base(
            convertToProviderExpression: x => x.ToString(),
            convertFromProviderExpression: x => Ulid.Parse(x))
    {
    }
}