using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs.Base;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs;

public class ItemDePedidoConfig : AuditableEntityConfig<ItemDePedido>
{
    protected override void ConfigureFields(EntityTypeBuilder<ItemDePedido> builder)
    {
        builder.Property(p => p.Nome).IsRequired();
        builder.Property(p => p.Descricao).IsRequired();
        builder.Property(p => p.Quantidade).IsRequired();
        builder.Property(p => p.Observacao);
        
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