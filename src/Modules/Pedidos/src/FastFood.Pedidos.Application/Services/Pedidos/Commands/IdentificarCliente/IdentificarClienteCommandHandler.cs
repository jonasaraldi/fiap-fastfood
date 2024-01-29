using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;
using MediatR;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.IdentificarCliente;

public sealed class IdentificarClienteCommandHandler : ICommandHandler<IdentificarClienteCommand>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public IdentificarClienteCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        IdentificarClienteCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);
        
        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();

        Cliente cliente = Cliente.Criar(
            request.Nome, Email.Criar(request.Email));
        
        pedido.SetCliente(cliente);
        
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}