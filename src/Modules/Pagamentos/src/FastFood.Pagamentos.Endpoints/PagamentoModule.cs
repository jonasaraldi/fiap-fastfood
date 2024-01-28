using Carter;
using FastFood.Pagamentos.Application.Services.Pagamentos.Commands.AtualizarSituacaoDoPagamento;
using FastFood.Pagamentos.Application.Services.Pagamentos.Commands.RealizarPagamento;
using FastFood.Pagamentos.Application.Services.Pagamentos.Queries.GetSituacaoDoPagamento;
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
        });
        
        pagamentos.MapGet("situacao", async (
            Ulid pedidoId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            GetSituacaoDoPagamentoQuery query = new(pedidoId);
            var response = await sender.Send(query, cancellationToken);
            return TypedResults.Ok(response);
        });
        
        pagamentos.MapPut("situacao", async (
            Ulid pedidoId,
            [FromBody] AtualizarSituacaoDoPagamentoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            AtualizarSituacaoDoPagamentoCommand command = new(pedidoId, request.CodigoDaSituacao);
            await sender.Send(command, cancellationToken);
            return TypedResults.NoContent();
        });
    }
}