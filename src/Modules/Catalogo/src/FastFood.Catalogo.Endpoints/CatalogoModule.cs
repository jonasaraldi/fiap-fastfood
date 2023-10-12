using Carter;
using FastFood.Catalogo.Application.Produtos.Commands.CriarProduto;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Atendimento.Endpoints;

public class CatalogoModule : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pedido = app.MapGroup("produtos");
        
        pedido.MapGet("", () => 
            Results.Ok("Lista de produtos"));
        
        pedido.MapPost("", async (
            ISender sender,
            [FromBody]CriarProdutoCommand command,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
    }
}