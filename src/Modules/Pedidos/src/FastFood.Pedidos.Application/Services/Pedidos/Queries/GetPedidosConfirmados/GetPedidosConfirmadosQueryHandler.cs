using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Queries.GetPedidosConfirmados;

public class GetPedidosConfirmadosQueryHandler : ICommandHandler<GetPedidosConfirmadosQuery, GetPedidosConfirmadosResponse>
{
    private readonly IPedidoRespository _pedidoRespository;

    public GetPedidosConfirmadosQueryHandler(
        IPedidoRespository pedidoRespository)
    {
        _pedidoRespository = pedidoRespository;
    }
    
    public async Task<GetPedidosConfirmadosResponse> Handle(
        GetPedidosConfirmadosQuery request, CancellationToken cancellationToken)
    {
        var pedidosConfirmados = await _pedidoRespository.GetConfirmadosDeHojeAsync(cancellationToken);
        
        var pedidosConfirmadosResponse = pedidosConfirmados
            .Select(pedido => new PedidoConfirmadoResponse(
                pedido.Id,
                pedido.Codigo,
                pedido.UpdatedAt.Value,
                pedido.Itens
                    .Select(item => new ItemDePedidoConfirmadoResponse(
                        item.Id, 
                        item.Nome,
                        item.Descricao,
                        item.Quantidade, 
                        item.Observacao)
                )));
        
        return new(pedidosConfirmadosResponse);
    }
}