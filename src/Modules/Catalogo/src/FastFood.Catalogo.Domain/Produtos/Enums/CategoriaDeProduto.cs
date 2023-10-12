using System.Reflection;

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

    public string Codigo { get; set; }
    public string Nome { get; set; }

    public static CategoriaDeProduto? Get(string codigo) => 
        codigo switch 
        {
            nameof(Lanche) => Lanche,
            nameof(Acompanhamento) => Acompanhamento,
            nameof(Bebida) => Bebida,
            nameof(Sobremesa) => Sobremesa,
            _ => null
        };
}
