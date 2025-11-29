using System;

namespace Pagamento.Domain
{
    public class TentativaPagamento
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataTentativa { get; private set; }
        public string Status { get; private set; } // Ex: Aprovado, Recusado, Pendente

        // Construtor privado para ser usado por ORM/deserialização
        private TentativaPagamento() { }

        public TentativaPagamento(Guid id, Guid pedidoId, decimal valor, DateTime dataTentativa, string status)
        {
            Id = id;
            PedidoId = pedidoId;
            Valor = valor;
            DataTentativa = dataTentativa;
            Status = status;
        }

        public void AtualizarStatus(string novoStatus)
        {
            Status = novoStatus;
        }
    }
}