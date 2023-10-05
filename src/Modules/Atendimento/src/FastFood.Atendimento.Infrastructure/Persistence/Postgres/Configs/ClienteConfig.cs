using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs;

public class ClienteConfig : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable(typeof(Cliente).Name);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion<UlidToStringConverter>();
        
        builder.Property(p => p.CreatedAt).IsRequired();

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