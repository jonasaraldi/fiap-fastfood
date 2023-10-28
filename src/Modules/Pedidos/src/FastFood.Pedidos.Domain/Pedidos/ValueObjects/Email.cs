namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects;

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
    
    public static implicit operator string(Email email) => email.Valor;
}