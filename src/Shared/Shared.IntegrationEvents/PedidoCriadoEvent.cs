using System;
using System.Collections.Generic;

namespace Shared.IntegrationEvents
{
    public class PedidoCriadoEvent
    {
        public Guid PedidoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorTotal { get; set; }
        public IEnumerable<ItemPedidoEventData> Itens { get; set; }

        public PedidoCriadoEvent(Guid pedidoId, DateTime dataCriacao, decimal valorTotal, IEnumerable<ItemPedidoEventData> itens)
        {
            PedidoId = pedidoId;
            DataCriacao = dataCriacao;
            ValorTotal = valorTotal;
            Itens = itens;
        }
    }

    public class ItemPedidoEventData
    {
        public Guid ItemId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public ItemPedidoEventData(Guid itemId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            ItemId = itemId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
    }
}