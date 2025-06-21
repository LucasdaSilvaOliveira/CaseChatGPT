using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Messaging
{
    public interface IRabbitMQConsumer
    {
        void Consumer<T>(string queue);
    }
}
