using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;

namespace FastFood.Atendimento.Application.Pedidos.Commands.AdicionarItemDePedido;

public class AdicionarItemDePedidoCommandHandler : ICommandHandler<AdicionarItemDePedidoCommand, AdicionarItemDePedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarItemDePedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AdicionarItemDePedidoResponse> Handle(
        AdicionarItemDePedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);

        if (pedido is null) 
            throw new PedidoNaoEncontradoDomainException();

        ItemDePedido item = ItemDePedido.Criar(
            request.Nome, 
            request.Descricao, 
            Dinheiro.Criar(request.Preco),
            request.Quantidade);
        
        pedido.AdicionarItem(item);

        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new(pedido.Id, item.Id);
    }
}