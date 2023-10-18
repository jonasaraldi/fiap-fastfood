namespace FastFood.Atendimento.Endpoints.Models;

public sealed record IdentificarClienteRequest(
    string Nome, 
    string Email)
{
}