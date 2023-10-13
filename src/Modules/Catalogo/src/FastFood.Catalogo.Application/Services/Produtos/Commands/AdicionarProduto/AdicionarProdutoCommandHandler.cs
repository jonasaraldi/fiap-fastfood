using FastFood.Catalogo.Application.Abstractions;
using FastFood.Catalogo.Application.Abstractions.UnitsOfWork;
using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Enums;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using FastFood.Catalogo.Domain.Produtos.ValueObjects;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.CriarProduto;

public class AdicionarProdutoCommandHandler 
    : ICommandHandler<AdicionarProdutoCommand, AdicionarProdutoResponse>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarProdutoCommandHandler(
        IProdutoRepository produtoRepository, 
        IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AdicionarProdutoResponse> Handle(
        AdicionarProdutoCommand request, CancellationToken cancellationToken)
    {
        Produto produto = Produto.Criar(
            request.Nome, 
            request.Descricao, 
            CategoriaDeProduto.Get(request.Categoria)!,
            Dinheiro.Criar(request.Preco), 
            Url.Criar(request.UrlDaImagem));
        
        await _produtoRepository.AddAsync(produto, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new(produto.Id);
    }
}