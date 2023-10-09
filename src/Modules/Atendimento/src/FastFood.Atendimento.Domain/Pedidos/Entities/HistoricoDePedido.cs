using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Entities;

public class HistoricoDePedido : Entity
{
    private HistoricoDePedido()
    {
    }
    
    public HistoricoDePedido(Pedido pedido)
    {
        SetPedido(pedido);
        Status = pedido.Status;
        Data = pedido.UpdatedAt ?? pedido.CreatedAt;
    }

    public Pedido Pedido { get; private set; }
    public Ulid PedidoId { get; private set; }
    public StatusDePedido Status { get; private set; }
    public DateTime Data { get; private set; }

    private void SetPedido(Pedido pedido)
    {
        Pedido = Pedido;
        PedidoId = pedido.Id;
    }
    
    public static HistoricoDePedido Criar(Pedido pedido) => new(pedido);
}