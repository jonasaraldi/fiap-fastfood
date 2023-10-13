using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using MediatR;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.AtualizarCpf;

public sealed class AtualizarCpfCommandHandler : ICommandHandler<AtualizarCpfCommand>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public AtualizarCpfCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(
        AtualizarCpfCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);

        if (pedido is null) 
            throw new PedidoNaoEncontradoDomainException();

        var cpf = Cpf.Criar(request.Cpf);
        pedido.SetCpf(cpf);
        
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}