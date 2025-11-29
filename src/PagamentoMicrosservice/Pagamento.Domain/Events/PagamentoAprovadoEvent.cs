using System;

namespace Pagamento.Domain.Events
{
    public class PagamentoAprovadoEvent //: IDomainEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid TentativaPagamentoId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataAprovacao { get; private set; }

        public PagamentoAprovadoEvent(Guid pedidoId, Guid tentativaPagamentoId, decimal valor, DateTime dataAprovacao)
        {
            PedidoId = pedidoId;
            TentativaPagamentoId = tentativaPagamentoId;
            Valor = valor;
            DataAprovacao = dataAprovacao;
        }
    }
}