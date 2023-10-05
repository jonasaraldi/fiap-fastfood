using MediatR;

namespace FastFood.Atendimento.Application.Abstractions;

public interface ICommandHandler : IRequestHandler<ICommand>
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
}