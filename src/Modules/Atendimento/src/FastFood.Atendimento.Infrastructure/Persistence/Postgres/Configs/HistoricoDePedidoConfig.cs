using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Base;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Configs;

public class HistoricoDePedidoConfig : EntityConfig<HistoricoDePedido>
{
    protected override void ConfigureFields(EntityTypeBuilder<HistoricoDePedido> builder)
    {
        builder.Property(p => p.Data).IsRequired();
        
        builder.Property(p => p.Status)
            .HasConversion<StatusDePedidoToStringConverter>()
            .IsRequired();        
        
        builder.Property(p => p.PedidoId)
            .HasConversion<UlidToStringConverter>()
            .IsRequired();

        builder.HasOne(p => p.Pedido)
            .WithMany(p => p.Historicos)
            .HasForeignKey(p => p.PedidoId)
            .IsRequired();
    }
}