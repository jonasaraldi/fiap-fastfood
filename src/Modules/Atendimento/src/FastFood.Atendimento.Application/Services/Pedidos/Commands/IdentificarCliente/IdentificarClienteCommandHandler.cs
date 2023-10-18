using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using MediatR;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.IdentificarCliente;

public class IdentificarClienteCommandHandler : ICommandHandler<IdentificarClienteCommand>
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
    
    public async Task<Unit> Handle(
        IdentificarClienteCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);
        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();

        Cliente cliente = Cliente.Criar(
            request.Nome, Email.Criar(request.Email));
        
        pedido.SetCliente(cliente);
        
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}