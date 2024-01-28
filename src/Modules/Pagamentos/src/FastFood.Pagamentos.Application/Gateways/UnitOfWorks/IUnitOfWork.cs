namespace FastFood.Pagamentos.Application.Gateways.UnitOfWorks;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
}