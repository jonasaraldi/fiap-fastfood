using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Queries.GetConsultaDePedidos;

public sealed class GetConsultaDePedidosQueryHandler : ICommandHandler<GetConsultaDePedidosQuery, GetConsultaDePedidosResponse>
{
    private readonly IPedidoRespository _pedidoRespository;

    public GetConsultaDePedidosQueryHandler(
        IPedidoRespository pedidoRespository)
    {
        _pedidoRespository = pedidoRespository;
    }
    
    public async Task<GetConsultaDePedidosResponse> Handle(
        GetConsultaDePedidosQuery request, CancellationToken cancellationToken)
    {
        DateTime dataInicial = GetDataInicial(request);
        DateTime dataFinal = GetDataFinal(request);
        
        var pedidos = await _pedidoRespository.GetPorDataAsync(
            dataInicial, dataFinal, cancellationToken);
        
        var pedidosResponse = pedidos
            .Select(pedido => new PedidoResponse(
                pedido.Id,
                pedido.Codigo,
                pedido.Status.Descricao,
                pedido.CreatedAt,
                pedido.UpdatedAt,
                pedido.ValorTotal,
                pedido.Itens
                    .Select(item => new ItemDePedidoResponse(
                        item.Id,
                        item.Nome,
                        item.Descricao,
                        item.Preco,
                        item.Quantidade, 
                        item.Observacao)
                    ),
                pedido.Historicos
                    .Select(historico => new HistoricoDePedidoResponse(
                        historico.Status.Descricao,
                        historico.Data)
                    )
                ));

        return new(dataInicial, dataFinal, pedidosResponse);
    }

    private DateTime GetDataInicial(GetConsultaDePedidosQuery request)
    {
        DateTime dataInicialLimite = DateTime.UtcNow.AddMonths(-1).Date;
        DateTime dataInicial = (request.DataInicial is null || request.DataInicial < dataInicialLimite)
            ? dataInicialLimite : request.DataInicial.Value;

        return dataInicial;
    }
    
    private DateTime GetDataFinal(GetConsultaDePedidosQuery request)
    {
        DateTime dataFinalLimite = DateTime.UtcNow;
        DateTime dataFinal = (request.DataFinal is null || request.DataFinal > dataFinalLimite)
            ? dataFinalLimite : request.DataFinal.Value;

        return dataFinal;
    }
}