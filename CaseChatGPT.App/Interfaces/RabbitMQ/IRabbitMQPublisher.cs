using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.App.Interfaces.RabbitMQ
{
    public interface IRabbitMQPublisher
    {
        Task Publish<T>(T message, string routingKey, string queue);
    }
}
