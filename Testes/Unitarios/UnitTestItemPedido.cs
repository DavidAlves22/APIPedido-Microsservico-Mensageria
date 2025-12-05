using Pedido.Domain;

namespace Testes.Unitarios
{
    public class UnitTestItemPedido
    {
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente()
        {
            // Arrange
            var id = Guid.NewGuid();
            var pedidoId = Guid.NewGuid();
            var nomeProduto = "Produto Teste";
            var quantidade = 5;
            var precoUnitario = 10.5m;

            // Act
            var itemPedido = new ItemPedido(id, pedidoId, nomeProduto, quantidade, precoUnitario);

            // Assert
            Assert.Equal(id, itemPedido.Id);
            Assert.Equal(pedidoId, itemPedido.PedidoId);
            Assert.Equal(nomeProduto, itemPedido.NomeProduto);
            Assert.Equal(quantidade, itemPedido.Quantidade);
            Assert.Equal(precoUnitario, itemPedido.PrecoUnitario);
        }

        [Theory]
        [InlineData("", 1, 10.5)]
        [InlineData(null, 1, 10.5)]
        [InlineData("Produto Teste", 0, 10.5)]
        [InlineData("Produto Teste", -1, 10.5)]
        [InlineData("Produto Teste", 1, -10.5)]
        public void Construtor_DeveLancarExcecao_SeParametrosInvalidos(string nomeProduto, int quantidade, decimal precoUnitario)
        {
            // Arrange
            var id = Guid.NewGuid();
            var pedidoId = Guid.NewGuid();

            // Act-Assert
            Assert.Throws<ArgumentException>(() => new ItemPedido(id, pedidoId, nomeProduto, quantidade, precoUnitario));
        }
    }
}