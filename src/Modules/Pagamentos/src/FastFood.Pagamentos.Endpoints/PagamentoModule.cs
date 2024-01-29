using Carter;
using FastFood.Pagamentos.Application.UseCases.Pagamentos.Commands.AtualizarSituacaoDoPagamento;
using FastFood.Pagamentos.Application.UseCases.Pagamentos.Commands.RealizarPagamento;
using FastFood.Pagamentos.Application.UseCases.Pagamentos.Queries.GetSituacaoDoPagamento;
using FastFood.Pagamentos.Endpoints.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Pagamentos.Endpoints;

public class PagamentoModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pagamentos = app.MapGroup("pagamentos/{pedidoId}")
            .WithTags("Pagamento");

        pagamentos.MapPost("", async (
            Ulid pedidoId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            RealizarPagamentoCommand command = new(pedidoId); 
            await sender.Send(command, cancellationToken);
            return TypedResults.NoContent();
        })
        .WithSummary("Realizar pagamento")
        .WithDescription("Realiza pagamento do pedido.");
        
        pagamentos.MapGet("situacao", async (
            Ulid pedidoId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            GetSituacaoDoPagamentoQuery query = new(pedidoId);
            var response = await sender.Send(query, cancellationToken);
            return TypedResults.Ok(response);
        })
        .WithSummary("Obtem situação de pagamento")
        .WithDescription("Obtem situação de pagamento pedido.");
        
        pagamentos.MapPost("situacao", async (
            Ulid pedidoId,
            [FromBody] AtualizarSituacaoDoPagamentoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            AtualizarSituacaoDoPagamentoCommand command = new(pedidoId, request.CodigoDaSituacao);
            await sender.Send(command, cancellationToken);
            return TypedResults.NoContent();
        })
        .WithSummary("Webhook de pagamento")
        .WithDescription("Webhook de atualização da situação do pagamento.");
    }
}