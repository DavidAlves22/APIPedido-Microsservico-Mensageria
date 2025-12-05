using Pedido.Domain;

namespace Testes.Unitarios
{
    public class UnitTestPedido
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AtualizarStatus_Erro_SeInformarVazioOuNulo(string status)
        {
            // Arrange
            var pedido = new Pedido.Domain.Pedido(Guid.NewGuid(), DateTime.Now, "Pendente", 100);

            // Act-Assert
            var exception = Assert.Throws<ArgumentException>(() => pedido.AtualizarStatus(status));
            Assert.Contains("O status não pode ser nulo ou vazio.", exception.Message);
        }

        [Fact]
        public void AtualizarStatus_AlteraStatus_SeInformarValorValido()
        {
            // Arrange
            var pedido = new Pedido.Domain.Pedido(Guid.NewGuid(), DateTime.Now, "Pendente", 100);
            var novoStatus = "Aprovado";

            // Act
            pedido.AtualizarStatus(novoStatus);

            // Assert
            Assert.Equal(novoStatus, pedido.Status);
        }

        [Fact]
        public void AdicionarItem_AdicionaItemNaLista()
        {
            // Arrange
            var pedido = new Pedido.Domain.Pedido(Guid.NewGuid(), DateTime.Now, "Pendente", 0);
            var item = new ItemPedido(Guid.NewGuid(), pedido.Id, "Produto Teste", 2, 50);

            // Act
            pedido.AdicionarItem(item);

            // Assert
            Assert.Single(pedido.Itens);
            Assert.Contains(item, pedido.Itens);
        }

        [Fact]
        public void AdicionarItem_AtualizaValorTotal()
        {
            // Arrange
            var pedido = new Pedido.Domain.Pedido(Guid.NewGuid(), DateTime.Now, "Pendente", 0);
            var item1 = new ItemPedido(Guid.NewGuid(), pedido.Id, "Produto 1", 2, 50);
            var item2 = new ItemPedido(Guid.NewGuid(), pedido.Id, "Produto 2", 1, 100);

            // Act
            pedido.AdicionarItem(item1);
            pedido.AdicionarItem(item2);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact]
        public void AdicionarItem_Erro_SeItemNulo()
        {
            // Arrange
            var pedido = new Pedido.Domain.Pedido(Guid.NewGuid(), DateTime.Now, "Pendente", 0);

            // Act-Assert
            var exception = Assert.Throws<ArgumentNullException>(() => pedido.AdicionarItem(null));
            Assert.Contains("item", exception.ParamName);
        }
    }
}
