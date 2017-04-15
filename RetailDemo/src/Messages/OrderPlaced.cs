using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;

namespace Messages
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
    }
}
