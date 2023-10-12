namespace FastFood.Catalogo.Domain.Produtos.ValueObjects;

public sealed class Dinheiro
{
    private const string MoedaBrasileira = "BRL";

    private Dinheiro()
    {
    }

    private Dinheiro(decimal valor, string moeda)
    {
        Valor = valor;
        Moeda = moeda;
    }

    public decimal Valor { get; }
    public string Moeda { get; }

    public static Dinheiro Criar(decimal valor)
    {
        return new(valor, MoedaBrasileira);
    }

    public static implicit operator decimal(Dinheiro dinheiro) => dinheiro.Valor;
}