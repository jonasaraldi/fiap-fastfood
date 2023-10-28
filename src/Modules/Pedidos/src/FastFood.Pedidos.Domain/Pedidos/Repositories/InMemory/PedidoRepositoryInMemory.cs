using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Domain.Pedidos.Repositories.InMemory;

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

    public Task<ICollection<Pedido>> GetPorDataAsync(
        DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken)
    {
        ICollection<Pedido> pedidos = Pedidos
            .Where(p => p.CreatedAt >= dataInicial && p.CreatedAt <= dataFinal)
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

        return Task.FromResult(pedidos);
    }
}