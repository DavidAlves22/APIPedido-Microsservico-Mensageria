using System.Threading.Tasks;

namespace Pagamento.Application.Interfaces
{
    public interface IEventBus
    {
        Task Publish<T>(T message) where T : class;
    }
}