using FastFood.Catalogo.Application.Abstractions;
using FastFood.Catalogo.Application.Abstractions.UnitsOfWork;
using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Enums;
using FastFood.Catalogo.Domain.Produtos.Exceptions;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using FastFood.Catalogo.Domain.Produtos.ValueObjects;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.EditarProduto;

public class EditarProdutoCommandHandler : ICommandHandler<EditarProdutoCommand, EditarProdutoResponse>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarProdutoCommandHandler(
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<EditarProdutoResponse> Handle(
        EditarProdutoCommand request, CancellationToken cancellationToken)
    {
        Produto? produto = await _produtoRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (produto is null)
            throw new ProdutoNaoEncontradoDomainException();
        
        CategoriaDeProduto categoria = CategoriaDeProduto.Get(request.Categoria)!;
        Dinheiro preco = Dinheiro.Criar(request.Preco);
        Url urlDaImagem = Url.Criar(request.UrlDaImagem);

        produto
            .SetNome(request.Nome)
            .SetDescricao(request.Descricao)
            .SetCategoria(categoria)
            .SetPreco(preco)
            .SetUrlDaImagem(urlDaImagem);
        
        _produtoRepository.Update(produto);
        await _unitOfWork.CommitAsync(cancellationToken); 
        
        return new(produto.Id);
    }
}