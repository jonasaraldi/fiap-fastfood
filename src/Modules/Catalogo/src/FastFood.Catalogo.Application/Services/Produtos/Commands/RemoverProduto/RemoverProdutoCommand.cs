using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.RemoverProduto;

public record RemoverProdutoCommand(Ulid Id) : ICommand
{
}