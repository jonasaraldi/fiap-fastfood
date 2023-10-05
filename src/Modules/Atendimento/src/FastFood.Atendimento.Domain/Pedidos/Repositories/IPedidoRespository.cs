namespace FastFood.Atendimento.Domain.Pedidos.Repositories;

public interface IPedidoRespository
{
    Task AddAsync(Pedido pedido, CancellationToken cancellationToken);
}