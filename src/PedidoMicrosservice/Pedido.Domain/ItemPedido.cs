using System;

namespace Pedido.Domain
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        // Construtor privado para ser usado por ORM/deserialização
        private ItemPedido() { }

        public ItemPedido(Guid id, Guid pedidoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            Id = id;
            PedidoId = pedidoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
    }
}