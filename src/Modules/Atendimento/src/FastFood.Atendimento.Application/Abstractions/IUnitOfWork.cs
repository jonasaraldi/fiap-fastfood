namespace FastFood.Atendimento.Application.Abstractions;

public interface IUnitOfWork
{
    public Task CommitAsync();
}