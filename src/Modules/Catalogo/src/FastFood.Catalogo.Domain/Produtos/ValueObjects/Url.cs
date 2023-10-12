namespace FastFood.Catalogo.Domain.Produtos.ValueObjects;

public sealed class Url
{
    private Url()
    {
    }

    private Url(string caminho)
    {
        Caminho = caminho;
    }

    public string Caminho { get; }
    
    public static Url Criar(string caminho) => new(caminho);
    
    public static implicit operator string(Url url) => url.Caminho;
}