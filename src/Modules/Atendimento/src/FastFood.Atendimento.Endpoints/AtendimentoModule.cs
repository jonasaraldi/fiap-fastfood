using Carter;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.AdicionarItemDePedido;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.AtualizarCpf;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.CancelarPedido;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.ConfirmarPedido;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.CriarPedido;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.FinalizarPedido;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.RemoverItemDePedido;
using FastFood.Atendimento.Application.Services.Pedidos.Queries.GetConsultaDePedidos;
using FastFood.Atendimento.Application.Services.Pedidos.Queries.GetPedidosConfirmados;
using FastFood.Atendimento.Endpoints.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Atendimento.Endpoints;

public class AtendimentoModule : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pedido = app.MapGroup("pedidos");
        
        pedido.MapGet("", async (
            [FromQuery]DateTime? dataInicial,
            [FromQuery]DateTime? dataFinal,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetConsultaDePedidosQuery(
                dataInicial, dataFinal);
            
            var response = await sender.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapGet("confirmados", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetPedidosConfirmadosQuery(), cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapPut("{pedidoId}/cliente", () => 
            Results.Ok("Informar nome e email do cliente no pedido"));
        
        pedido.MapPost("", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new CriarPedidoCommand(), cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapPut("{pedidoId}/cpf", async (
            Ulid pedidoId,
            [FromBody]AtualizarCpfRequest request,
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new AtualizarCpfCommand(pedidoId, request.cpf);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        });
        
        pedido.MapPost("{pedidoId}/itens", async (
            Ulid pedidoId, 
            [FromBody]AdicionarItemDePedidoRequest request, 
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            AdicionarItemDePedidoCommand command = new(
                pedidoId, request.Nome, request.Descricao, request.Preco, request.Quantidade, request.Observacao);
            
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
        
        pedido.MapPut("{pedidoId}/confirmar", async (
            Ulid pedidoId,
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new ConfirmarPedidoCommand(pedidoId);
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        pedido.MapPut("{pedidoId}/cancelar", async (
            Ulid pedidoId,
            ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new CancelarPedidoCommand(pedidoId);
            var response = await sender.Send(command, cancellationToken);
            return Results.Ok(response);
        });
        
        // pedido.MapPut("{pedidoId}/finalizar", async (
        //     Ulid pedidoId,
        //     ISender sender, 
        //     CancellationToken cancellationToken) =>
        // {
        //     var command = new FinalizarPedidoCommand(pedidoId);
        //     var response = await sender.Send(command, cancellationToken);
        //     return Results.Ok(response);
        // });
    }
}