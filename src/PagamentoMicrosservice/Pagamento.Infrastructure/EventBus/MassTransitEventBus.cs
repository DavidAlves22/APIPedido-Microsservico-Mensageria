using MassTransit;
using Pagamento.Application.Interfaces;

namespace Pagamento.Infrastructure.EventBus
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IBus _bus;

        public MassTransitEventBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message) where T : class
        {
            await _bus.Publish(message);
        }
    }
}