using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    public class OrderCreatedHandler : IHandleMessages<OrderPlaced>
    {

        static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {

            // ...WE GOT ONE...!
            log.Info($"Handling: OrderPlaced for Order Id: {message.OrderId}");

            //return Task.CompletedTask; <-- REQUIRES .Net 4.6

            var completedTask = Task.FromResult(0);
            return completedTask;
        }
    }
}
