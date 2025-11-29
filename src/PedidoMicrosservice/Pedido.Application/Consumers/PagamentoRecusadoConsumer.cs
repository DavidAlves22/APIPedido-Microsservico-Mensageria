using MassTransit;
using Pagamento.Domain;
using Pagamento.Domain.Events;
using Pedido.Application.UseCases;

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
                NovoStatus = StatusPagamento.Recusado.ToString()
            });

            // Opcional: Logar o recebimento do evento
            // Console.WriteLine($"PagamentoRecusadoEvent recebido para PedidoId: {message.PedidoId}. Motivo: {message.Motivo}");
        }
    }
}