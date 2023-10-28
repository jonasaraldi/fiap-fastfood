namespace FastFood.Pedidos.Domain.Pedidos.Repositories;

public interface IPedidoRespository
{
    Task AddAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<Pedido?> GetByIdAsync(Ulid id, CancellationToken cancellationToken);
    void Update(Pedido pedido);
    Task<ICollection<Pedido>> GetConfirmadosDeHojeAsync(CancellationToken cancellationToken);
    Task<ICollection<Pedido>> GetPorDataAsync(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken);
}