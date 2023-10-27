using Carter;
using FastFood.Catalogo.Application.Services.Categorias.Queries.GetCategorias;
using FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;
using FastFood.Catalogo.Application.Services.Produtos.Commands.CriarProduto;
using FastFood.Catalogo.Application.Services.Produtos.Commands.EditarProduto;
using FastFood.Catalogo.Application.Services.Produtos.Commands.RemoverProduto;
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
        var categoria = app.MapGroup("categorias")
            .WithTags("Catalogo");

        categoria.MapGet("", async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(
                    new GetCategoriasQuery(), cancellationToken);

                return Results.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Consulta de categorias",
                Description = "Retorna todas as categorias de produto disponíveis."
            });

        var produto = app.MapGroup("produtos")
            .WithTags("Catalogo");
        
        produto.MapGet("categoria/{categoria}", async (
                string categoria,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(
                    new GetProdutosPorCategoriaQuery(categoria), cancellationToken);
                
                return Results.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Consulta de produtos por categoria",
                Description = "Retorna todos os produtos da categoria informada."
            });
        
        produto.MapPost("", async (
                ISender sender,
                [FromBody]ProdutoRequest request,
                CancellationToken cancellationToken) =>
            {
                var command = new AdicionarProdutoCommand(
                    request.Nome, 
                    request.Descricao, 
                    request.Categoria, 
                    request.Preco, 
                    request.UrlDaImagem);
                
                var response = await sender.Send(command, cancellationToken);
                return Results.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Adiciona um novo produto",
                Description = "Adiciona um novo produto ao catálogo."
            });
        
        produto.MapPut("{id}", async (
                Ulid id,
                [FromBody]ProdutoRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new EditarProdutoCommand(
                    id,
                    request.Nome, 
                    request.Descricao, 
                    request.Categoria, 
                    request.Preco, 
                    request.UrlDaImagem);
                
                await sender.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edita um produto",
                Description = "Edita um produto específico do catálogo."
            });
        
        produto.MapDelete("{id}", async (
                Ulid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoverProdutoCommand(id);
                
                await sender.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Remove um produto",
                Description = "Remove um produto específico do catálogo."
            });
    }
}