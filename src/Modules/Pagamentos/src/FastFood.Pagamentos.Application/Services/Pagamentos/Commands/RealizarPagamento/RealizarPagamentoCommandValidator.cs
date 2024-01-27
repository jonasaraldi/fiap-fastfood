using FluentValidation;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.RealizarPagamento;

public sealed class RealizarPagamentoCommandValidator : AbstractValidator<RealizarPagamentoCommand>
{
    public RealizarPagamentoCommandValidator()
    {
        RuleFor(p => p.PedidoId)
            .NotEmpty()
            .WithMessage("Pedido não informado");
    }
}