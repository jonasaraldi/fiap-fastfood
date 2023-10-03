namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects;

public sealed record Cpf
{
    private Cpf(string valor)
    {
        Valor = valor;
    }
    
    public string Valor { get; }

    public static Cpf Criar(string valor)
    {
        return new(valor);
    }
}