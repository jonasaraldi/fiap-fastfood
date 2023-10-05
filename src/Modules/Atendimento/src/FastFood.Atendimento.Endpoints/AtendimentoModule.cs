using Carter;
using FastFood.Atendimento.Application.Pedidos.Commands.CriarPedido;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Pedido.Endpoints;

public class PedidoModule : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pedido = app.MapGroup("pedidos");
        
        pedido.MapGet("", () => 
            Results.Ok("Lista de pedidos"));
        
        pedido.MapPost("", async (ISender sender) =>
        {
            var response = await sender.Send(new CriarPedidoCommand());
            return Results.Ok(response.PedidoId);
        });
        
        pedido.MapPost("itens", () => 
            Results.Ok("Novo item adicionado"));
        
        pedido.MapPatch("confirmado", () => 
            Results.Ok("Situacao do pedido alterada para confirmada"));
        
        pedido.MapPatch("finalizado", () => 
            Results.Ok("Situacao do pedido alterada para finalizada"));
    }
}