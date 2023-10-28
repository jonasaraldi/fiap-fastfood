namespace FastFood.Pedidos.Application.Abstractions.UnitsOfWork;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
}