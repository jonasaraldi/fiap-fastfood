using FluentValidation;

namespace FastFood.Pagamentos.Application.UseCases.Pagamentos.Queries.GetSituacaoDoPagamento;

public class GetSituacaoDoPagamentoQueryValidator : AbstractValidator<GetSituacaoDoPagamentoQuery>
{
    public GetSituacaoDoPagamentoQueryValidator()
    {
        RuleFor(p => p.PedidoId)
            .NotEmpty()
            .WithMessage("Pedido não informado");
    }
}