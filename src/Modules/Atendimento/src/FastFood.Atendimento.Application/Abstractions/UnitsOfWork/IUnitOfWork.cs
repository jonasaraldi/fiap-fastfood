namespace FastFood.Atendimento.Application.Abstractions.UnitsOfWork;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
}