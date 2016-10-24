using NServiceBus;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain()
                .GetAwaiter()
                .GetResult();
        }

        static async Task AsyncMain()
        {

            string endpointName = "Samples.StepByStep.Client";

            Console.Title = endpointName;

            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers(); // Ask NServiceBus to automatically create message queues
            endpointConfiguration.UsePersistence<InMemoryPersistence>(); // Only used for subscription information for this example

            var endpointInstance = await Endpoint
                                            .Start(endpointConfiguration)
                                            .ConfigureAwait(false);

            try
            {
                await SendOrder(endpointInstance);
            }
            finally
            {
                await endpointInstance
                            .Stop()
                            .ConfigureAwait(false);
            }
        }

        static async Task SendOrder(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }

                var id = Guid.NewGuid();

                var placeOrder = new PlaceOrder
                {
                    Product = "New shoes",
                    Id = id
                };

                await endpointInstance.Send("Samples.StepByStep.Server", placeOrder);
                Console.WriteLine($"Sent a PlaceOrder message with id: {id:N}");
            }
        }
    }
}
