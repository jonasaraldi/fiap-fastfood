using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Exceptions;
using FastFood.Catalogo.Domain.Produtos.ValueObjects;
using FastFood.Catalogo.Domain.Produtos.ValueObjects.Categorias;
using FastFood.Contracts.Produtos;

namespace FastFood.Catalogo.Tests.Domain;

public class ProdutoTests
{
    [Fact]
    public void CriarProduto_ComPrecoMaiorQueZero_DeveCriarProduto()
    {
        var nome = "Hamburguer";
        var descricao = "Hamburguer de carne";
        var categoria = CategoriaDeProduto.Lanche;
        var preco = Dinheiro.Criar(20m);
        var urlDaImagem = Url.Criar("hamburguer.png");
        
        var produto = Produto.Criar(nome, descricao, categoria, preco, urlDaImagem);
        
        Assert.Equal(nome, produto.Nome);
        Assert.Equal(descricao, produto.Descricao);
        Assert.Equal(categoria, produto.Categoria);
        Assert.Equal(preco, produto.Preco);
        Assert.Equal(urlDaImagem, produto.UrlDaImagem);
        Assert.IsType<DomainEvents.ProdutoCriadoDomainEvent>(produto.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void CriarProduto_ComPrecoMenorQueZero_DeveLancarExcecao()
    {
        Assert.Throws<ProdutoDeveTerPrecoMaiorQueZeroDomainException>(() => CriarProduto(-1));
    }
    
    [Fact]
    public void CriarProduto_ComPrecoIgualAZero_DeveCriarProduto()
    {
        Assert.Throws<ProdutoDeveTerPrecoMaiorQueZeroDomainException>(() => CriarProduto(0));
    }

    private Produto CriarProduto(decimal preco) =>
        Produto.Criar(
            "Hamburguer", 
            "Pão, carne e queijo", 
            CategoriaDeProduto.Lanche, 
            Dinheiro.Criar(preco),
            Url.Criar("hamburguer.png"));
}