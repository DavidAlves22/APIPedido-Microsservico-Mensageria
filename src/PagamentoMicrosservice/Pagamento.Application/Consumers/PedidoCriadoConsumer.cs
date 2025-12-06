using MassTransit;
using Pagamento.Application.UseCases;
using Shared.IntegrationEvents;

namespace Pagamento.Application.Consumers
{
    public class PedidoCriadoConsumer : IConsumer<PedidoCriadoEvent>
    {
        private readonly ProcessarPagamentoUseCase _processarPagamentoUseCase;

        public PedidoCriadoConsumer(ProcessarPagamentoUseCase processarPagamentoUseCase)
        {
            _processarPagamentoUseCase = processarPagamentoUseCase;
        }

        public async Task Consume(ConsumeContext<PedidoCriadoEvent> context)
        {
            var message = context.Message;
            await _processarPagamentoUseCase.ExecuteAsync(new ProcessarPagamentoInput
            {
                PedidoId = message.PedidoId,
                Valor = message.ValorTotal
            });

            // Opcional: Logar o recebimento do evento
            // Console.WriteLine($"PedidoCriadoDomainEvent recebido para PedidoId: {message.PedidoId}");
        }
    }
}