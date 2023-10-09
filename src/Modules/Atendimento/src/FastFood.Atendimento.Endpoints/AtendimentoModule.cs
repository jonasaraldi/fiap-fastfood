using Carter;
using FastFood.Atendimento.Application.Pedidos.Commands.AdicionarItemDePedido;
using FastFood.Atendimento.Application.Pedidos.Commands.ConfirmarPedido;
using FastFood.Atendimento.Application.Pedidos.Commands.CriarPedido;
using FastFood.Atendimento.Application.Pedidos.Commands.RemoverItemDePedido;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Atendimento.Endpoints;

public class PedidoModule : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pedido = app.MapGroup("pedidos");
        
        pedido.MapGet("", () => 
            Results.Ok("Lista de pedidos"));
        
        pedido.MapPost("", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new CriarPedidoCommand(), cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapPost("{pedidoId}/itens", async (
            Ulid pedidoId, 
            [FromBody]AdicionarItemDePedidoCommand command, 
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapDelete("{pedidoId}/itens/{id}", async (
            Ulid pedidoId, 
            Ulid id, 
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new RemoverItemDePedidoCommand(pedidoId, id);
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapPut("{pedidoId}/confirmado", async (
            Ulid pedidoId,
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new ConfirmarPedidoCommand(pedidoId);
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapPatch("confirmado", () => 
            Results.Ok("Situacao do pedido alterada para confirmada"));
        
        pedido.MapPatch("finalizado", () => 
            Results.Ok("Situacao do pedido alterada para finalizada"));
    }
}