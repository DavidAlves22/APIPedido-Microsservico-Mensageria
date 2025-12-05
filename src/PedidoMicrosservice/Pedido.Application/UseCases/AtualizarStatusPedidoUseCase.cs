using Pedido.Application.Interfaces;
using PedidoMicrosservice.Pedido.Domain.Exceptions; // Adicionado

namespace Pedido.Application.UseCases
{
    public class AtualizarStatusPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AtualizarStatusPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task ExecuteAsync(AtualizarStatusPedidoInput input)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(input.PedidoId);

            if (pedido == null)
            {
                throw new PedidoNaoEncontradoException($"Pedido com ID {input.PedidoId} não encontrado.");
            }

            pedido.AtualizarStatus(input.NovoStatus);
            await _pedidoRepository.UpdateAsync(pedido);
        }
    }

    public class AtualizarStatusPedidoInput
    {
        public Guid PedidoId { get; set; }
        public string NovoStatus { get; set; }
    }
}