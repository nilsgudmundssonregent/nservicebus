using System;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Orders
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog logger = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            logger.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            logger.Info("Placing order...");
            Thread.Sleep(5*000);

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };

            logger.Info("Order placed!");
            return context.Publish(orderPlaced);
        }
    }
}