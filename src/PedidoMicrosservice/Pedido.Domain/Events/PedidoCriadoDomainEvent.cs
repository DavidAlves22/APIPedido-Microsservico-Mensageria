using System;
using System.Collections.Generic;

namespace Pedido.Domain.Events
{
    public class PedidoCriadoDomainEvent //: IDomainEvent (se usarmos uma interface base para eventos de domínio)
    {
        public Guid PedidoId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public decimal ValorTotal { get; private set; }
        public IEnumerable<ItemPedidoEventData> Itens { get; private set; }

        public PedidoCriadoDomainEvent(Guid pedidoId, DateTime dataCriacao, decimal valorTotal, IEnumerable<ItemPedidoEventData> itens)
        {
            PedidoId = pedidoId;
            DataCriacao = dataCriacao;
            ValorTotal = valorTotal;
            Itens = itens;
        }
    }

    public class ItemPedidoEventData
    {
        public Guid ItemId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        public ItemPedidoEventData(Guid itemId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            ItemId = itemId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
    }
}