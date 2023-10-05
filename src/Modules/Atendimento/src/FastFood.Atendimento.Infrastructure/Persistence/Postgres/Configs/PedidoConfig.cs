using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs;

public class PedidoConfig : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable(typeof(Pedido).Name);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion<UlidToStringConverter>();
        
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.Codigo).IsRequired();
        builder.Property(p => p.ClienteId)
            .HasConversion<UlidToStringConverter>();
        
        builder.Property(p => p.Status)
            .HasConversion<StatusDePedidoToStringConverter>()
            .IsRequired();

        builder.HasMany(p => p.Itens)
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