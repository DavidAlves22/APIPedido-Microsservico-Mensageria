using System;
using System.Threading.Tasks;
using Pagamento.Application.Interfaces;
using Pagamento.Domain;
using Pagamento.Domain.Events;
using PagamentoMicrosservice.Pagamento.Domain.Exceptions; // Adicionado

namespace Pagamento.Application.UseCases
{
    public class ProcessarPagamentoUseCase
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IEventBus _eventBus;

        public ProcessarPagamentoUseCase(IPagamentoRepository pagamentoRepository, IEventBus eventBus)
        {
            _pagamentoRepository = pagamentoRepository;
            _eventBus = eventBus;
        }

        public async Task ExecuteAsync(ProcessarPagamentoInput input)
        {
            // Simula o processamento externo de pagamento
            bool pagamentoAprovado = new Random().Next(0, 2) == 1; // 50% de chance de aprovar

            var tentativaPagamento = new TentativaPagamento(
                Guid.NewGuid(),
                input.PedidoId,
                input.Valor,
                DateTime.UtcNow,
                pagamentoAprovado ? StatusPagamento.Aprovado.ToString() : StatusPagamento.Recusado.ToString()
            );

            await _pagamentoRepository.AddAsync(tentativaPagamento);

            if (pagamentoAprovado)
            {
                var pagamentoAprovadoEvent = new PagamentoAprovadoEvent(
                    tentativaPagamento.PedidoId,
                    tentativaPagamento.Id,
                    tentativaPagamento.Valor,
                    tentativaPagamento.DataTentativa
                );
                await _eventBus.Publish(pagamentoAprovadoEvent);
            }
            else
            {
                var mensagemRecusa = "Pagamento recusado pela operadora.";
                var pagamentoRecusadoEvent = new PagamentoRecusadoEvent(
                    tentativaPagamento.PedidoId,
                    tentativaPagamento.Id,
                    tentativaPagamento.Valor,
                    tentativaPagamento.DataTentativa,
                    mensagemRecusa
                );
                await _eventBus.Publish(pagamentoRecusadoEvent);
                throw new PagamentoInvalidoException(mensagemRecusa); // Lança a exceção de domínio
            }
        }
    }

    public class ProcessarPagamentoInput
    {
        public Guid PedidoId { get; set; }
        public decimal Valor { get; set; }
    }
}