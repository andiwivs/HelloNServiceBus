using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
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

            string endpointName = "Samples.StepByStep.Server";

            Console.Title = endpointName;

            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.HeartbeatPlugin("particular.servicecontrol");

            if (Shared.Config.USE_AZURE_INSTEAD_OF_MSMQ)
            {
                endpointConfiguration.UseTransport<AzureServiceBusTransport>()
                                            .ConnectionStringName("NServiceBus/Transport")
                                            .UseTopology<ForwardingTopology>();
            }

            var endpointInstance = await Endpoint
                                            .Start(endpointConfiguration)
                                            .ConfigureAwait(false);

            try
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance
                            .Stop()
                            .ConfigureAwait(false);
            }
        }
    }
}
