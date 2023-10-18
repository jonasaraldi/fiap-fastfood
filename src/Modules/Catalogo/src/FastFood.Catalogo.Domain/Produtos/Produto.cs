using FastFood.Catalogo.Domain.Produtos.Enums;
using FastFood.Catalogo.Domain.Produtos.Exceptions;
using FastFood.Catalogo.Domain.Produtos.ValueObjects;
using FastFood.Contracts.Abstractions;
using FastFood.Contracts.Produtos;

namespace FastFood.Catalogo.Domain.Produtos;

public sealed class Produto : AggregateRoot
{
    public const int NomeMaxLength = 100;
    public const int DescricaoMaxLength = 500;
    public const int UrlDaImagemMaxLength = 500;
    
    private Produto()
    {
    }

    private Produto(string nome, string descricao, CategoriaDeProduto categoria, Dinheiro preco, Url urlDaImagem)
    {
        SetNome(nome);
        SetDescricao(descricao);
        SetCategoria(categoria);
        SetPreco(preco);
        SetUrlDaImagem(urlDaImagem);
        
        RaiseDomainEvent(new DomainEvents.ProdutoCriado(
            Id, Nome, Descricao, Categoria.Nome, Preco, UrlDaImagem));
    }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public CategoriaDeProduto Categoria { get; private set; }
    public Dinheiro Preco { get; private set; }
    public Url UrlDaImagem { get; private set; }
    
    public Produto SetNome(string nome)
    {
        Nome = nome;
        return this;
    }
    
    public Produto SetDescricao(string descricao)
    {
        Descricao = descricao;
        return this;
    }
    
    public Produto SetCategoria(CategoriaDeProduto categoria)
    {   
        Categoria = categoria;
        return this;
    }
    
    public Produto SetPreco(Dinheiro preco)
    {
        if (preco <= 0)
            throw new ProdutoDeveTerPrecoMaiorQueZeroDomainException();
        
        Preco = preco;
        return this;
    }
    
    public Produto SetUrlDaImagem(Url urlDaImagem)
    {
        UrlDaImagem = urlDaImagem;
        return this;
    }
    
    public static Produto Criar(
        string nome, string descricao, CategoriaDeProduto categoria, Dinheiro preco, Url urlDaImagem)
    {
        return new(nome, descricao, categoria, preco, urlDaImagem);
    }
}