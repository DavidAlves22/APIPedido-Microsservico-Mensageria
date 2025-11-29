using Pedido.Application.Interfaces;
using Pedido.Domain;
using Pedido.Domain.Events;

namespace Pedido.Application.UseCases
{
    public class CriarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEventBus _eventBus;

        public CriarPedidoUseCase(IPedidoRepository pedidoRepository, IEventBus eventBus)
        {
            _pedidoRepository = pedidoRepository;
            _eventBus = eventBus;
        }

        public async Task ExecuteAsync(CriarPedidoInput input)
        {
            var itensPedido = input.Itens.Select(item => new ItemPedido(
                Guid.NewGuid(),
                input.PedidoId,
                item.NomeProduto,
                item.Quantidade,
                item.PrecoUnitario
            )).ToList();

            var pedido = new Pedido.Domain.Pedido(
                input.PedidoId,
                DateTime.UtcNow,
                StatusPedido.Pendente.ToString(),
                0
            );

            // Calcula o valor total dentro da entidade Pedido
            foreach (var item in itensPedido)
            {
                pedido.AdicionarItem(item);
            }

            await _pedidoRepository.AddAsync(pedido);

            var pedidoCriadoEvent = new PedidoCriadoDomainEvent(
                pedido.Id,
                pedido.DataCriacao,
                pedido.ValorTotal,
                pedido.Itens.Select(item => new ItemPedidoEventData(item.Id, item.NomeProduto, item.Quantidade, item.PrecoUnitario))
            );
            await _eventBus.Publish(pedidoCriadoEvent);
        }
    }

    public class CriarPedidoInput
    {
        public Guid PedidoId { get; set; }
        public List<ItemPedidoInput> Itens { get; set; }
    }

    public class ItemPedidoInput
    {
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}