using FluentValidation;

namespace FastFood.Catalogo.Application.Produtos.Commands.CriarProduto;

public class CriarProdutoValidator : AbstractValidator<CriarProdutoCommand>
{
    public CriarProdutoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome não informado")
            .MaximumLength(100)
            .WithMessage("Nome ultrapassou o limite de 100 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Descrição não informada")
            .MaximumLength(500)
            .WithMessage("Descrição ultrapassou o limite de 500 caracteres");

        RuleFor(x => x.Categoria)
            .NotEmpty()
            .WithMessage("Categoria não informada");

        RuleFor(x => x.Preco)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que zero");

        RuleFor(x => x.UrlDaImagem)
            .NotEmpty()
            .WithMessage("Url da imagem não informada")
            .MaximumLength(500)
            .WithMessage("Url da imagem ultrapassou o limite de 500 caracteres");
    }
}