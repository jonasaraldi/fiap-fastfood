using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs;

public class ItemDePedidoConfig : IEntityTypeConfiguration<ItemDePedido>
{
    public void Configure(EntityTypeBuilder<ItemDePedido> builder)
    {
        builder.ToTable(typeof(ItemDePedido).Name);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion<UlidToStringConverter>();
        
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.Nome).IsRequired();
        builder.Property(p => p.Descricao).IsRequired();
        builder.Property(p => p.Quantidade).IsRequired();
        
        builder.Property(p => p.PedidoId)
            .HasConversion<UlidToStringConverter>()
            .IsRequired();
        
        builder.OwnsOne(p => p.Preco)
            .Property(p => p.Valor)
            .HasColumnName(nameof(ItemDePedido.Preco));

        builder.HasOne(p => p.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(p => p.PedidoId)
            .IsRequired();
    }
}