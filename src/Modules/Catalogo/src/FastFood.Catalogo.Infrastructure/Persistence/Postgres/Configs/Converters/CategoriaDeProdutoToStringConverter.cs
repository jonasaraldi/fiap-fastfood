using FastFood.Catalogo.Domain.Produtos.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFood.Catalogo.Infrastructure.Persistence.Postgres.Configs.Converters;

public class CategoriaDeProdutoToStringConverter : ValueConverter<CategoriaDeProduto, string>
{
    public CategoriaDeProdutoToStringConverter() : base(
        convertToProviderExpression: categoriaDeProduto => categoriaDeProduto.Codigo,
        convertFromProviderExpression: codigo => CategoriaDeProduto.Get(codigo))
    {   
    }
}