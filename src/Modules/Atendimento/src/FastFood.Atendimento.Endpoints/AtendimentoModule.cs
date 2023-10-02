using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FastFood.Pedido.Endpoints;

public class PedidoModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var pedido = app.MapGroup("pedidos");
        
        pedido.MapGet("", () => 
            Results.Ok("Lista de pedidos"));
        
        pedido.MapPost("", () => 
            Results.Ok("Pedido salvo"));
        
        pedido.MapPost("itens", () => 
            Results.Ok("Novo item adicionado"));
        
        pedido.MapPatch("confirmado", () => 
            Results.Ok("Situacao do pedido alterada para confirmada"));
        
        pedido.MapPatch("finalizado", () => 
            Results.Ok("Situacao do pedido alterada para finalizada"));
    }
}