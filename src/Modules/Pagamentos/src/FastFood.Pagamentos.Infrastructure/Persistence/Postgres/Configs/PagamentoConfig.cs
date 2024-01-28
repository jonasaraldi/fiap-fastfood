using FastFood.Pagamentos.Domain;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Configs.Base;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Configs;

public class PagamentoConfig : AuditableEntityConfig<Pagamento>
{
    protected override void ConfigureFields(EntityTypeBuilder<Pagamento> builder)
    {
        builder.Property(p => p.PedidoId)
            .HasConversion<UlidToStringConverter>()
            .IsRequired();

        builder.Property(p => p.Situacao)
            .HasConversion<SituacaoDoPagamentoToStringConverter>()
            .IsRequired();

        builder.HasIndex(p => p.PedidoId)
            .IsUnique();
    }
}