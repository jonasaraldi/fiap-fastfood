using Carter;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.AdicionarItemDePedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.AtualizarCpf;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.CancelarPedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.ConfirmarPedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.CriarPedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.FinalizarPedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.FinalizarPreparoDoPedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.IdentificarCliente;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.PrepararPedido;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.RemoverItemDePedido;
using FastFood.Pedidos.Application.Services.Pedidos.Queries.GetConsultaDePedidos;
using FastFood.Pedidos.Application.Services.Pedidos.Queries.GetConsultaOperacionalDePedidos;
using FastFood.Pedidos.Endpoints.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Pedidos.Endpoints;

public class PedidoModule : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pedido = app.MapGroup("pedidos")
            .WithTags("Pedido");

        pedido.MapGet("", async (
                [FromQuery] DateTime? dataInicial,
                [FromQuery] DateTime? dataFinal,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetConsultaDePedidosQuery(
                    dataInicial, dataFinal);

                var response = await sender.Send(query, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Consulta de pedidos",
                Description = "Retorna uma lista de pedidos dentro de um período estipulado. Caso o período não seja informado, por padrão é retornado todos os pedidos criados dentro de um mês."
            });
        
        pedido.MapGet("operacionais", async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetPedidosOperacionaisQuery(), cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Consulta a fila de pedidos em operação",
                Description = "Retorna todos os pedidos prontos, em preparação e recebidos pela cozinha no dia atual."
            });

        pedido.MapPut("{pedidoId}/cliente", async (
                Ulid pedidoId,
                [FromBody]IdentificarClienteRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                IdentificarClienteCommand command = new(pedidoId, request.Nome, request.Email); 
                await sender.Send(command, cancellationToken);
                return TypedResults.NoContent();
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Registra cliente (nome e e-mail)",
                Description = "Registra o cliente na base com nome e e-mail."
            });
        
        pedido.MapPost("", async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new CriarPedidoCommand(), cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Cria um novo pedido",
                Description = "Cria um novo pedido do zero, sem itens e cliente. Essa é a chamada que da início a todo o processo do pedido."
            });

        pedido.MapPut("{pedidoId}/cpf", async (
                Ulid pedidoId,
                [FromBody]AtualizarCpfRequest request,
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                AtualizarCpfCommand command = new(pedidoId, request.cpf);
                await sender.Send(command, cancellationToken);
                return TypedResults.NoContent();
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Registra o CPF do cliente",
                Description = "Registra o CPF do cliente para posteriormente gera a nota fiscal com o CPF."
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
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Adiciona item ao pedido",
                Description = "Adiciona um novo item ao pedido informado."
            });
        
        pedido.MapDelete("{pedidoId}/itens/{id}", async (
                Ulid pedidoId, 
                Ulid id, 
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                RemoverItemDePedidoCommand command = new(pedidoId, id);
                var response = await sender.Send(command, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Remove item do pedido",
                Description = "Remove um item específico do pedido informado."
            });
        
        pedido.MapPut("{pedidoId}/confirmar", async (
                Ulid pedidoId,
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                ConfirmarPedidoCommand command = new(pedidoId);
                var response = await sender.Send(command, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Confirma o pedido",
                Description = "Confirma o pedido indicando que ele está pronto para ser feito pela cozinha (após pago)."
            });
        
        pedido.MapPut("{pedidoId}/cancelar", async (
                Ulid pedidoId,
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                CancelarPedidoCommand command = new(pedidoId);
                var response = await sender.Send(command, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Cancela o pedido",
                Description = "Cancela o pedido informado caso já não esteja confirmado."
            });
        
        pedido.MapPut("{pedidoId}/iniciar-preparo", async (
                Ulid pedidoId,
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                PrepararPedidoCommand command = new(pedidoId);
                var response = await sender.Send(command, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Preparar o pedido",
                Description = "Inicia o preparo do pedido informado caso já tenha sido pago."
            });
        
        pedido.MapPut("{pedidoId}/finalizar-preparo", async (
                Ulid pedidoId,
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                FinalizarPreparoDoPedidoCommand command = new(pedidoId);
                var response = await sender.Send(command, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Finalizar preparo do pedido",
                Description = "Finaliza o preparo do pedido informado marcando ele como pronto."
            });
        
        pedido.MapPut("{pedidoId}/finalizar", async (
                Ulid pedidoId,
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                FinalizarPedidoCommand command = new(pedidoId);
                var response = await sender.Send(command, cancellationToken);
                return TypedResults.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Finalizar pedido",
                Description = "Finaliza o pedido após o pedido ser entregue para o cliente."
            });
    }
}