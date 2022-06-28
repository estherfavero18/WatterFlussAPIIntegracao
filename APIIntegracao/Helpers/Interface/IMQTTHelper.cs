using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Helpers.Interface
{
    public interface IMQTTHelper
    {
        Task Subscribe(string topico);
        Task<MqttClientPublishResult> Publish(string topico);
    }
}
