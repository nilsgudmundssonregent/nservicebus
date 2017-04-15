using System.Threading;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog logger = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            logger.Info($"Received OrderPlaced, OrderId = {message.OrderId}");
            logger.Info("Charching credit card...");
            Thread.Sleep(20*1000);

            var orderBilled = new OrderBilled
            {
                OrderId = message.OrderId,
            };
            logger.Info("Credit card charged!");

            return context.Publish(orderBilled);
        }
    }
}