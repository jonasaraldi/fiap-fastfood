using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Produtos.Commands.CriarProduto;

public class CriarProdutoCommandHandler : ICommandHandler<CriarProdutoCommand, CriarProdutoResponse>
{
    public Task<CriarProdutoResponse> Handle(
        CriarProdutoCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CriarProdutoResponse(Ulid.NewUlid()));
    }
}