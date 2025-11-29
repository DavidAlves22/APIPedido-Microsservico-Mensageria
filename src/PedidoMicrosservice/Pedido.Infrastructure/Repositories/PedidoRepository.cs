using Pedido.Application.Interfaces;

namespace Pedido.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private static readonly List<Pedido.Domain.Pedido> _pedidos = new List<Pedido.Domain.Pedido>();

        public Task AddAsync(Pedido.Domain.Pedido pedido)
        {
            _pedidos.Add(pedido);
            return Task.CompletedTask;
        }

        public Task<Pedido.Domain.Pedido> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_pedidos.FirstOrDefault(p => p.Id == id));
        }

        public Task UpdateAsync(Pedido.Domain.Pedido pedido)
        {
            var existingPedido = _pedidos.FirstOrDefault(p => p.Id == pedido.Id);
            if (existingPedido != null)
            {
                _pedidos.Remove(existingPedido);
                _pedidos.Add(pedido);
            }
            return Task.CompletedTask;
        }
    }
}