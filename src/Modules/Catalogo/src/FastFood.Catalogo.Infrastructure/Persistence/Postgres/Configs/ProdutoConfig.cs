using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Enums;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Configs.Base;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Catalogo.Infrastructure.Persistence.Postgres.Configs;

public class ProdutoConfig : AuditableEntityConfig<Produto>
{
    protected override void ConfigureFields(EntityTypeBuilder<Produto> builder)
    {
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(Produto.NomeMaxLength);
        
        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(Produto.DescricaoMaxLength);
        
        builder.Property(p => p.Categoria)
            .HasConversion<CategoriaDeProdutoToStringConverter>()
            .IsRequired()
            .HasColumnName(nameof(Produto.Categoria));
        
        builder.OwnsOne(p => p.Preco)
            .Property(p => p.Valor)
            .IsRequired()
            .HasColumnName(nameof(Produto.Preco));

        builder.OwnsOne(p => p.UrlDaImagem)
            .Property(p => p.Caminho)
            .IsRequired()
            .HasMaxLength(Produto.UrlDaImagemMaxLength)
            .HasColumnName(nameof(Produto.UrlDaImagem));
        
        builder.HasIndex(nameof(Produto.Categoria));
    }
}