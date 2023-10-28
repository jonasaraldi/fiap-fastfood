using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres.Configs;

public class ClienteConfig : AuditableEntityConfig<Cliente>
{
    protected override void ConfigureFields(EntityTypeBuilder<Cliente> builder)
    {
        builder.Property(p => p.Nome).IsRequired();
        
        builder.OwnsOne(p => p.Email)
            .Property(p => p.Valor)
            .HasColumnName(nameof(Cliente.Email))
            .IsRequired();
        
        builder.HasMany(p => p.Pedidos)
            .WithOne(p => p.Cliente)
            .IsRequired();
    }
}