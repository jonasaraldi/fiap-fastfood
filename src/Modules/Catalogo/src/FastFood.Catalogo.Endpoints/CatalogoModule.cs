using Carter;
using FastFood.Catalogo.Application.Produtos.Commands.CriarProduto;
using FastFood.Catalogo.Application.Produtos.Queries.GetProdutosPorCategoria;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Catalogo.Endpoints;

public class CatalogoModule : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var produto = app.MapGroup("produtos");
        
        produto.MapGet("{categoria}", async (
            string categoria,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(
                new GetProdutosPorCategoriaQuery(categoria), cancellationToken);
            
            return Results.Ok(response);
        });
        
        produto.MapPost("", async (
            ISender sender,
            [FromBody]CriarProdutoCommand command,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
    }
}