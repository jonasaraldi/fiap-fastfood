using Carter;
using FastFood.Catalogo.Application.Services.Categorias.Queries.GetCategorias;
using FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;
using FastFood.Catalogo.Application.Services.Produtos.Commands.CriarProduto;
using FastFood.Catalogo.Application.Services.Produtos.Queries.GetProdutosPorCategoria;
using FastFood.Catalogo.Endpoints.Models;
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
        var categoria = app.MapGroup("categorias");
        
        categoria.MapGet("", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(
                new GetCategoriasQuery(), cancellationToken);
            
            return Results.Ok(response);
        });
        
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
        
        produto.MapPut("{id}", async (
            Ulid id,
            [FromBody]AtualizarProdutoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AtualizarProdutoCommand(
                id, request.Nome, request.Descricao, request.Categoria, request.Preco, request.UrlDaImagem);
            
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
    }
}