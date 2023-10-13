using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.AtualizarCpf;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

public class AtualizarCpfCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveAtualizarCpfNoPedido()
    {
        Pedido pedido = Pedido.Criar();
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        await repository.AddAsync(pedido, CancellationToken.None);
        
        string cpf = "07838053064";
        AtualizarCpfCommand command = new(pedido.Id, cpf);
        AtualizarCpfCommandHandler handler = new(repository, unitOfWork);
        
        await handler.Handle(command, CancellationToken.None);
        
        var pedidoPosUpdate = await repository.GetByIdAsync(pedido.Id, CancellationToken.None);
        
        Assert.Equal(pedido.Id, pedidoPosUpdate!.Id);
        Assert.Equal(cpf, pedidoPosUpdate.Cpf!.Valor);
    }
    
    [Fact]
    public async Task Handle_NaoDeveAtualizarCpfNoPedido_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        string cpf = "07838053064";
        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        AtualizarCpfCommand command = new(pedidoIdAleatorio, cpf);
        AtualizarCpfCommandHandler handler = new(repository, unitOfWork);
        
        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("12345678901")]
    public async Task Handle_NaoDeveAtualizarCpfNoPedido_QuandoCpfForInvalido(string? cpf)
    {
        Pedido pedido = Pedido.Criar();
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        await repository.AddAsync(pedido, CancellationToken.None);
        
        AtualizarCpfCommand command = new(pedido.Id, cpf);
        AtualizarCpfCommandHandler handler = new(repository, unitOfWork);
        
        await Assert.ThrowsAsync<CpfInvalidoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }
}