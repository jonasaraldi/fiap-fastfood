using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Queries.GetConsultaOperacionalDePedidos;

public class GetPedidosOperacionaisQueryHandler 
    : ICommandHandler<GetPedidosOperacionaisQuery, GetPedidosOperacionaisResponse>
{
    private readonly IPedidoRespository _pedidoRespository;

    public GetPedidosOperacionaisQueryHandler(
        IPedidoRespository pedidoRespository)
    {
        _pedidoRespository = pedidoRespository;
    }
    
    public async Task<GetPedidosOperacionaisResponse> Handle(
        GetPedidosOperacionaisQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await _pedidoRespository.GetPedidosEmOperacaoAsync(cancellationToken);
        
        var pedidosResponse = pedidos
            .OrderByDescending(p => p.Status.Ordem)
            .ThenBy(p => p.CreatedAt)
            .Select(pedido => new PedidoOperacionalResponse(
                pedido.Id,
                pedido.Codigo,
                pedido.Status.Descricao,
                pedido.CreatedAt,
                pedido.UpdatedAt,
                pedido.ValorTotal,
                pedido.Pago,
                pedido.Itens
                    .Select(item => new ItemDePedidoOperacionalResponse(
                        item.Id,
                        item.Nome,
                        item.Descricao,
                        item.Preco,
                        item.Quantidade, 
                        item.Observacao)
                    )
            ));

        return new GetPedidosOperacionaisResponse(pedidosResponse);
    }
}