namespace FastFood.Catalogo.Application.Abstractions.UnitsOfWork;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
}