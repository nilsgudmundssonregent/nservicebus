using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Stock
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog logger = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            logger.Info($"Received OrderPlaced, OrderId = {message.OrderId}");

            logger.Info($"Updating stock and perparing to ship...");

            return Task.CompletedTask;
        }
    }
}