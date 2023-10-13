using FluentValidation;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.AtualizarCpf;

public class AtualizarCpfValidator : AbstractValidator<AtualizarCpfCommand>
{
    public AtualizarCpfValidator()
    {   
        RuleFor(p => p.PedidoId)
            .NotEmpty()
            .WithMessage("Id do pedido não informado");
        
        RuleFor(p => p.Cpf)
            .NotEmpty()
            .WithMessage("CPF não informado");
    }
}