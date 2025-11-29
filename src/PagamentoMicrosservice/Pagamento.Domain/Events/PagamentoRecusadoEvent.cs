using System;

namespace Pagamento.Domain.Events
{
    public class PagamentoRecusadoEvent //: IDomainEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid TentativaPagamentoId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataRecusa { get; private set; }
        public string Motivo { get; private set; }

        public PagamentoRecusadoEvent(Guid pedidoId, Guid tentativaPagamentoId, decimal valor, DateTime dataRecusa, string motivo)
        {
            PedidoId = pedidoId;
            TentativaPagamentoId = tentativaPagamentoId;
            Valor = valor;
            DataRecusa = dataRecusa;
            Motivo = motivo;
        }
    }
}