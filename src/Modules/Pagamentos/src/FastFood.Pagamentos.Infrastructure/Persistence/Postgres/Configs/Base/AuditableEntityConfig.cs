using FastFood.Contracts.Abstractions;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Configs.Base;

public abstract class AuditableEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditableEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion<UlidToStringConverter>();
        
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        
        ConfigureFields(builder);
    }

    protected abstract void ConfigureFields(EntityTypeBuilder<TEntity> builder);
}