namespace FastFood.Atendimento.Domain.Pedidos.Repositories;

public interface IPedidoRespository
{
    Task AddAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<Pedido?> GetByIdAsync(Ulid id, CancellationToken cancellationToken);
    void Update(Pedido pedido);
}