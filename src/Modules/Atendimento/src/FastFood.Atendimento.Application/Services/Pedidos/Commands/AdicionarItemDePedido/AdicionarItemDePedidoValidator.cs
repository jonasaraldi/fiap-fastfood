using FluentValidation;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.AdicionarItemDePedido;

public class AdicionarItemDePedidoValidator : AbstractValidator<AdicionarItemDePedidoCommand>
{
    public AdicionarItemDePedidoValidator()
    {   
        RuleFor(p => p.PedidoId)
            .NotEmpty()
            .WithMessage("Id do pedido não informado");
        
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("Nome não informado");
        
        RuleFor(p => p.Descricao)
            .NotEmpty()
            .WithMessage("Descrição não informada");
        
        RuleFor(p => p.Preco)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que zero");
        
        RuleFor(p => p.Quantidade)
            .GreaterThan(0)
            .WithMessage("Quantidade deve ser maior que zero");
    }
}