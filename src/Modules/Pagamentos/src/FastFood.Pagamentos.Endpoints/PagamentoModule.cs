using Carter;
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
        var pagamentos = app.MapGroup("pagamentos")
            .WithTags("Pagamento");

        pagamentos.MapPut("status", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            return TypedResults.Ok();
        });
    }
}