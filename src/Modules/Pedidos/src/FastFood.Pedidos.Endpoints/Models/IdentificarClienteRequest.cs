namespace FastFood.Pedidos.Endpoints.Models;

public sealed record IdentificarClienteRequest(
    string Nome, 
    string Email)
{
}