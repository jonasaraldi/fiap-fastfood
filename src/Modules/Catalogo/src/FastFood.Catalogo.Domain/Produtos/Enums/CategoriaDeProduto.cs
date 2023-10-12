namespace FastFood.Catalogo.Domain.Produtos.Enums;

public class CategoriaDeProduto
{
    public static CategoriaDeProduto Lanche => new(nameof(Lanche), "Lanche");
    public static CategoriaDeProduto Acompanhamento => new(nameof(Acompanhamento), "Acompanhamento");
    public static CategoriaDeProduto Bebida => new(nameof(Bebida), "Bebida");
    public static CategoriaDeProduto Sobremesa => new(nameof(Sobremesa), "Sobremesa");

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

    public static CategoriaDeProduto? Get(string codigo)
    {
        return codigo switch
        {
            nameof(Lanche) => Lanche,
            nameof(Acompanhamento) => Acompanhamento,
            nameof(Bebida) => Bebida,
            nameof(Sobremesa) => Sobremesa,
            _ => null
        };
    }

    public static ICollection<CategoriaDeProduto> GetAll() =>
        new List<CategoriaDeProduto>
        {
            Lanche,
            Acompanhamento,
            Bebida,
            Sobremesa
        };
}
