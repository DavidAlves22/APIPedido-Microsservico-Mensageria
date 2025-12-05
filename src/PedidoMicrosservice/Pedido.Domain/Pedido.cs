using System;
using System.Collections.Generic;

namespace Pedido.Domain
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string Status { get; private set; }
        public decimal ValorTotal { get; private set; }
        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();

        // Construtor privado para ser usado por ORM/deserialização
        private Pedido() { }

        public Pedido(Guid id, DateTime dataCriacao, string status, decimal valorTotal)
        {
            Id = id;
            DataCriacao = dataCriacao;
            Status = status;
            ValorTotal = valorTotal;
        }

        public void AtualizarStatus(string novoStatus)
        {
            if (string.IsNullOrEmpty(novoStatus))
                throw new ArgumentException("O status não pode ser nulo ou vazio.", nameof(novoStatus));

            Status = novoStatus;
        }

        public void AdicionarItem(ItemPedido item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            Itens.Add(item);
            CalcularValorTotal();
        }

        private void CalcularValorTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
            {
                total += item.Quantidade * item.PrecoUnitario;
            }
            ValorTotal = total;
        }
    }
}