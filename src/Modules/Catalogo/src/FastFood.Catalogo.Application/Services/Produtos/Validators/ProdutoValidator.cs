using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Enums;
using FluentValidation;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;

public abstract class ProdutoValidator<TProdutoValidator> : AbstractValidator<TProdutoValidator>
    where TProdutoValidator : ProdutoCommand
{
    public ProdutoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome não informado")
            .MaximumLength(Produto.NomeMaxLength)
            .WithMessage($"Nome ultrapassou o limite de {Produto.NomeMaxLength} caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Descrição não informada")
            .MaximumLength(Produto.DescricaoMaxLength)
            .WithMessage($"Descrição ultrapassou o limite de {Produto.DescricaoMaxLength} caracteres");

        RuleFor(x => x.Categoria)
            .NotEmpty()
            .WithMessage("Categoria não informada")
            .Must(codigoDeCategoria => CategoriaDeProduto.Get(codigoDeCategoria) != null)
            .WithMessage("Categoria inválida");

        RuleFor(x => x.Preco)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que zero");

        RuleFor(x => x.UrlDaImagem)
            .NotEmpty()
            .WithMessage("Url da imagem não informada")
            .MaximumLength(Produto.UrlDaImagemMaxLength)
            .WithMessage($"Url da imagem ultrapassou o limite de {Produto.UrlDaImagemMaxLength} caracteres");
    }
}