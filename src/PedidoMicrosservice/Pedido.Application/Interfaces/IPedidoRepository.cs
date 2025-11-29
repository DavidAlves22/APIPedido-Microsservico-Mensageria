using System;
using System.Threading.Tasks;
using Pedido.Domain;

namespace Pedido.Application.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido.Domain.Pedido> GetByIdAsync(Guid id);
        Task AddAsync(Pedido.Domain.Pedido pedido);
        Task UpdateAsync(Pedido.Domain.Pedido pedido);
    }
}