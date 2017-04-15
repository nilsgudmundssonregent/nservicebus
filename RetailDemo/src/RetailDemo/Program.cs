using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace RetailDemo
{
    public class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(@"Data Source=.\SQLEXPRESS;Initial Catalog=nservicetest;Integrated Security=true;");

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Orders");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);


            await RunLoop(endpointInstance);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static ILog logger = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                logger.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        logger.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.Send(command)
                            .ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        logger.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
