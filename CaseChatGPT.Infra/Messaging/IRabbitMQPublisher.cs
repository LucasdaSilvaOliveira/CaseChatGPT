using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Messaging
{
    internal interface IRabbitMQPublisher
    {
        void Publish<T>(T message, string routingKey, string queue);
    }
}
