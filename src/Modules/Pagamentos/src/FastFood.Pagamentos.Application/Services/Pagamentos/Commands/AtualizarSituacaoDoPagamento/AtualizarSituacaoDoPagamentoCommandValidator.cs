using FluentValidation;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.AtualizarSituacaoDoPagamento;

public class AtualizarSituacaoDoPagamentoCommandValidator 
    : AbstractValidator<AtualizarSituacaoDoPagamentoCommand>
{
    public AtualizarSituacaoDoPagamentoCommandValidator()
    {
        RuleFor(p => p.PedidoId)
            .NotEmpty()
            .WithMessage("Pedido não informado");
        
        RuleFor(p => p.CodigoDaSituacao)
            .NotEmpty()
            .WithMessage("Código da situação não informado");
    }
}