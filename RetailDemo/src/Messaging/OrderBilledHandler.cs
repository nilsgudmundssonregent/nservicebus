using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Messaging
{
    public class OrderBilledHandler : IHandleMessages<OrderBilled>
    {
        static ILog logger = LogManager.GetLogger<OrderBilledHandler>();

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            logger.Info($"Received OrderBilled, OrderId = {message.OrderId}");

            logger.Info($"Sending confirmation to customer!");
            return Task.CompletedTask;
        }
    }
}