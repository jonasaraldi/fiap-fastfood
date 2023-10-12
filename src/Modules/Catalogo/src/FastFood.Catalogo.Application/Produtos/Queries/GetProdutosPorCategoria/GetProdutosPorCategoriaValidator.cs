using FastFood.Catalogo.Domain.Produtos.Enums;
using FluentValidation;

namespace FastFood.Catalogo.Application.Produtos.Queries.GetProdutosPorCategoria;

public class GetProdutosPorCategoriaValidator : AbstractValidator<GetProdutosPorCategoriaQuery>
{
    public GetProdutosPorCategoriaValidator()
    {
        RuleFor(x => x.Categoria)
            .NotEmpty()
            .WithMessage("Categoria não informada")
            .Must(codigoDeCategoria => CategoriaDeProduto.Get(codigoDeCategoria) != null)
            .WithMessage("Categoria inválida");
    }
}