using FluentValidation;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;

public sealed class AtualizarProdutoValidator : ProdutoValidator<AtualizarProdutoCommand>
{
    public AtualizarProdutoValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Id não informado");
    }
}