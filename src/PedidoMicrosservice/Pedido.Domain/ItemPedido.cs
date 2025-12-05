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
            SetNomeProduto(nomeProduto);
            SetQuantidade(quantidade);
            SetPrecoUnitario(precoUnitario);
        }

        // Métodos SET com validações
        public void SetNomeProduto(string nomeProduto)
        {
            if (string.IsNullOrWhiteSpace(nomeProduto))
                throw new ArgumentException("O nome do produto não pode ser vazio ou nulo.", nameof(nomeProduto));
            NomeProduto = nomeProduto;
        }

        public void SetQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));
            Quantidade = quantidade;
        }

        public void SetPrecoUnitario(decimal precoUnitario)
        {
            if (precoUnitario < 0)
                throw new ArgumentException("O preço unitário não pode ser negativo.", nameof(precoUnitario));
            PrecoUnitario = precoUnitario;
        }
    }
}