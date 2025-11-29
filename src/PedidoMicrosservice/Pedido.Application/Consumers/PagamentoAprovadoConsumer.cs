using MassTransit;
using Pagamento.Domain;
using Pagamento.Domain.Events;
using Pedido.Application.UseCases;

namespace Pedido.Application.Consumers
{
    public class PagamentoAprovadoConsumer : IConsumer<PagamentoAprovadoEvent>
    {
        private readonly AtualizarStatusPedidoUseCase _atualizarStatusPedidoUseCase;

        public PagamentoAprovadoConsumer(AtualizarStatusPedidoUseCase atualizarStatusPedidoUseCase)
        {
            _atualizarStatusPedidoUseCase = atualizarStatusPedidoUseCase;
        }

        public async Task Consume(ConsumeContext<PagamentoAprovadoEvent> context)
        {
            var message = context.Message;
            await _atualizarStatusPedidoUseCase.ExecuteAsync(new AtualizarStatusPedidoInput
            {
                PedidoId = message.PedidoId,
                NovoStatus = StatusPagamento.Aprovado.ToString()
            });

            // Opcional: Logar o recebimento do evento
            // Console.WriteLine($"PagamentoAprovadoEvent recebido para PedidoId: {message.PedidoId}");
        }
    }
}