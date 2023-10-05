namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects;

public sealed record Email
{
    private Email()
    {
    }

    private Email(string valor)
    {
        Valor = valor;
    }

    public string Valor { get; }

    public static Email Criar(string valor)
    {
        return new(valor);
    }
}