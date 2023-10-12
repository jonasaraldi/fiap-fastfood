using MediatR;

namespace FastFood.Catalogo.Application.Abstractions;

public interface ICommand : IRequest
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
    where TResponse : class
{
}