using System.Threading.Tasks;

namespace Pedido.Application.Interfaces
{
    public interface IEventBus
    {
        Task Publish<T>(T message) where T : class;
    }
}