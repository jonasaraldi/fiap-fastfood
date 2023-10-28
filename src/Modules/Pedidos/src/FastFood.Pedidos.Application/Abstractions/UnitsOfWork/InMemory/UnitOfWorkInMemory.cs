namespace FastFood.Pedidos.Application.Abstractions.UnitsOfWork.InMemory;

public class UnitOfWorkInMemory : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}