using FastFood.Catalogo.Application.Abstractions;
using FastFood.Catalogo.Application.Abstractions.UnitsOfWork;
using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Exceptions;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using MediatR;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.RemoverProduto;

public class RemoverProdutoCommandHandler : ICommandHandler<RemoverProdutoCommand>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoverProdutoCommandHandler(
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(RemoverProdutoCommand request, CancellationToken cancellationToken)
    {
        Produto? produto = await _produtoRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (produto is null)
            throw new ProdutoNaoEncontradoDomainException();
        
        _produtoRepository.Remove(produto!);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}