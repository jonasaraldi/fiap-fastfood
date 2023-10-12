using FluentValidation;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.RemoverProduto;

public sealed class RemoverProdutoValidator : AbstractValidator<RemoverProdutoCommand>
{
    public RemoverProdutoValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Id não informado");
    }
}