using System;
using System.Threading.Tasks;
using Pagamento.Domain;

namespace Pagamento.Application.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<TentativaPagamento> GetByIdAsync(Guid id);
        Task AddAsync(TentativaPagamento tentativaPagamento);
        Task UpdateAsync(TentativaPagamento tentativaPagamento);
    }
}