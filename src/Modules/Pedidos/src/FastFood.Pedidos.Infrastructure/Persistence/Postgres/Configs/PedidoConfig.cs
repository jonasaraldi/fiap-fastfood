using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs.Base;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs;

public class PedidoConfig : AuditableEntityConfig<Pedido>
{
    protected override void ConfigureFields(EntityTypeBuilder<Pedido> builder)
    {
        builder.Property(p => p.Codigo).IsRequired();
        builder.Property(p => p.ClienteId)
            .HasConversion<UlidToStringConverter>();
        
        builder.Property(p => p.Status)
            .HasConversion<StatusDePedidoToStringConverter>()
            .IsRequired();

        builder.Property(p => p.Pago)
            .IsRequired();

        builder.HasMany(p => p.Itens)
            .WithOne(i => i.Pedido)
            .HasForeignKey(i => i.PedidoId);
        
        builder.HasMany(p => p.Historicos)
            .WithOne(i => i.Pedido)
            .HasForeignKey(i => i.PedidoId);

        builder.HasOne(p => p.Cliente)
            .WithMany(p => p.Pedidos)
            .HasForeignKey(p => p.ClienteId);

        builder.OwnsOne(p => p.Cpf)
            .Property(p => p.Valor)
            .HasColumnName(nameof(Pedido.Cpf));
        
        builder.HasIndex(p => p.Codigo).IsUnique();
    }
}