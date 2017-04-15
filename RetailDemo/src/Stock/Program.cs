using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using ServiceControl.Features;

namespace Stock
{
    public class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Stock";

            var endpointConfiguration = new EndpointConfiguration("Stock");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(@"Data Source=.\SQLEXPRESS;Initial Catalog=nservicetest;Integrated Security=true;");

            var routing = transport.Routing();
            routing.RegisterPublisher(typeof(OrderPlaced), "Orders");
            routing.RegisterPublisher(typeof(OrderBilled), "Billing");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.DisableFeature<Heartbeats>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);


            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
