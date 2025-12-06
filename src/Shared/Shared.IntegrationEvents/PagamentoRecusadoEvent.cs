using System;

namespace Shared.IntegrationEvents
{
    public class PagamentoRecusadoEvent
    {
        public Guid PedidoId { get; set; }
        public Guid TentativaPagamentoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataRecusa { get; set; }
        public string Motivo { get; set; }

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