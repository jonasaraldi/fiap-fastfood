using MediatR;

namespace FastFood.Atendimento.Application.Abstractions;

public interface ICommand : IRequest
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
    where TResponse : class
{
}