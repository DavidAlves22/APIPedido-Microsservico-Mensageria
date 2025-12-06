using MassTransit;
using Pedido.Application.UseCases;
using Shared.Core.Enums;
using Shared.IntegrationEvents;

namespace Pedido.Application.Consumers
{
    public class PagamentoRecusadoConsumer : IConsumer<PagamentoRecusadoEvent>
    {
        private readonly AtualizarStatusPedidoUseCase _atualizarStatusPedidoUseCase;

        public PagamentoRecusadoConsumer(AtualizarStatusPedidoUseCase atualizarStatusPedidoUseCase)
        {
            _atualizarStatusPedidoUseCase = atualizarStatusPedidoUseCase;
        }

        public async Task Consume(ConsumeContext<PagamentoRecusadoEvent> context)
        {
            var message = context.Message;
            await _atualizarStatusPedidoUseCase.ExecuteAsync(new AtualizarStatusPedidoInput
            {
                PedidoId = message.PedidoId,
                NovoStatus = StatusPagamentoEnum.Recusado.ToString()
            });

            // Opcional: Logar o recebimento do evento
            // Console.WriteLine($"PagamentoRecusadoEvent recebido para PedidoId: {message.PedidoId}. Motivo: {message.Motivo}");
        }
    }
}