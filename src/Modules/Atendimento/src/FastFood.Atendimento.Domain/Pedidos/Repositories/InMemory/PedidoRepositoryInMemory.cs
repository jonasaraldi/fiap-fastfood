namespace FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;

public class PedidoRepositoryInMemory : IPedidoRespository
{
    public static List<Pedido> Pedidos = new();
    
    public async Task AddAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        Pedidos.Add(pedido);
        await Task.CompletedTask;
    }
}