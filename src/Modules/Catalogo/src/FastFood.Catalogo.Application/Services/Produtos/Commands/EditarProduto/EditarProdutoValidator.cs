using FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;
using FluentValidation;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.EditarProduto;

public sealed class EditarProdutoValidator : ProdutoValidator<EditarProdutoCommand>
{
    public EditarProdutoValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Id não informado");
    }
}