using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagamento.Application.Interfaces;
using Pagamento.Domain;

namespace Pagamento.Infrastructure.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private static readonly List<TentativaPagamento> _tentativasPagamento = new List<TentativaPagamento>();

        public Task AddAsync(TentativaPagamento tentativaPagamento)
        {
            _tentativasPagamento.Add(tentativaPagamento);
            return Task.CompletedTask;
        }

        public Task<TentativaPagamento> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_tentativasPagamento.FirstOrDefault(tp => tp.Id == id));
        }

        public Task UpdateAsync(TentativaPagamento tentativaPagamento)
        {
            var existingTentativa = _tentativasPagamento.FirstOrDefault(tp => tp.Id == tentativaPagamento.Id);
            if (existingTentativa != null)
            {
                _tentativasPagamento.Remove(existingTentativa);
                _tentativasPagamento.Add(tentativaPagamento);
            }
            return Task.CompletedTask;
        }
    }
}