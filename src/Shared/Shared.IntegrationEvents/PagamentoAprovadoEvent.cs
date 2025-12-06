using System;

namespace Shared.IntegrationEvents
{
    public class PagamentoAprovadoEvent
    {
        public Guid PedidoId { get; set; }
        public Guid TentativaPagamentoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAprovacao { get; set; }

        public PagamentoAprovadoEvent(Guid pedidoId, Guid tentativaPagamentoId, decimal valor, DateTime dataAprovacao)
        {
            PedidoId = pedidoId;
            TentativaPagamentoId = tentativaPagamentoId;
            Valor = valor;
            DataAprovacao = dataAprovacao;
        }
    }
}