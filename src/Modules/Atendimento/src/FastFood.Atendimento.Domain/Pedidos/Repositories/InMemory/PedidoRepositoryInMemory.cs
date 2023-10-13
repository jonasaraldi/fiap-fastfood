using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;

public class PedidoRepositoryInMemory : IPedidoRespository
{
    public static List<Pedido> Pedidos = new();
    
    public async Task AddAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        Pedidos.Add(pedido);
        await Task.CompletedTask;
    }

    public Task<Pedido?> GetByIdAsync(Ulid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(Pedidos.FirstOrDefault(p => p.Id.Equals(id)));
    }

    public void Update(Pedido pedido)
    {
        int indexOf = Pedidos.IndexOf(pedido);
        if (indexOf < 0) return;
        
        Pedidos[indexOf] = pedido;
    }

    public Task<ICollection<Pedido>> GetConfirmadosDeHojeAsync(CancellationToken cancellationToken)
    {
        ICollection<Pedido> pedidosConfirmados = Pedidos
            .Where(p => p.Status.Equals(StatusDePedido.Confirmado) && p.UpdatedAt >= DateTime.UtcNow.Date)
            .OrderBy(p => p.UpdatedAt)
            .ToList();
        
        return Task.FromResult(pedidosConfirmados);
    }
}