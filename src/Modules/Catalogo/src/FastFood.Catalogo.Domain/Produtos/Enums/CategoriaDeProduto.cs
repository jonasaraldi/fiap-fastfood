namespace FastFood.Catalogo.Domain.Produtos.ValueObjects.Categorias;

public record CategoriaDeProduto
{
    private CategoriaDeProduto()
    {
    }
    
    private CategoriaDeProduto(string codigo, string nome)
    {
        Codigo = codigo;
        Nome = nome;
    }

    public string Codigo { get; }
    public string Nome { get; }
    
    public static CategoriaDeProduto Lanche => new(nameof(Lanche), "Lanche");
    public static CategoriaDeProduto Acompanhamento => new(nameof(Acompanhamento), "Acompanhamento");
    public static CategoriaDeProduto Bebida => new(nameof(Bebida), "Bebida");
    public static CategoriaDeProduto Sobremesa => new(nameof(Sobremesa), "Sobremesa");
}
