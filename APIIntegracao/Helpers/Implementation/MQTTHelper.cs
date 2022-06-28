using APIIntegracao.Helpers.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using System.Threading;
using MQTTnet.Protocol;
using System.Text;
using Microsoft.Extensions.Hosting;
using APIIntegracao.ApiCollection.Interface;

namespace APIIntegracao.Helpers.Implementation
{
    public class MQTTHelper : IHostedService
    {
        private readonly IConfiguration _configuration;
        private IMqttClient _mqttClient;
        private readonly IApiDatabase _database;
        public MQTTHelper(IConfiguration configuration, IApiDatabase database)
        {
            _configuration = configuration;
            _database = database;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                //Startup logic here
                var mqttFactory = new MqttFactory();
                _mqttClient = mqttFactory.CreateMqttClient();
                await ConnectClient();
                SetClientEvents();
                Subscribe("Distancia").Wait();
                Subscribe("VazaoEntrada").Wait();
                Subscribe("VazaoSaida").Wait();
            }
            catch
            {
                Console.WriteLine("Erro no StarAsync");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttClient.DisconnectAsync();

        }
        private async Task ConnectClient()
        {
            try
            {
                var options = new MqttClientOptionsBuilder()
                                    .WithClientId(_configuration["MQTT:ClientId"])
                                    .WithTcpServer(_configuration["MQTT:Host"], Convert.ToInt32(_configuration["MQTT:Port"]))
                                    .WithCredentials(_configuration["MQTT:Username"], _configuration["MQTT:Password"])
                                    .Build();
                await _mqttClient.ConnectAsync(options, CancellationToken.None);
            }
            catch
            {
                Console.WriteLine("Erro ao conectar");
            }

        }
        private void SetClientEvents()
        {
            Func<MqttApplicationMessageReceivedEventArgs, Task> messageReceivedEvent = async (args) => SalvaDados(args);
            _mqttClient.ApplicationMessageReceivedAsync += messageReceivedEvent;

            Func<MqttClientConnectedEventArgs, Task> connectedEvent = async (args) => Console.WriteLine("Conectado");
            _mqttClient.ConnectedAsync += connectedEvent;

            Func<MqttClientDisconnectedEventArgs, Task> disconnectedEvent = async (args) => Console.WriteLine("Desconectado");
            _mqttClient.DisconnectedAsync += disconnectedEvent;
        }
        public async Task Subscribe(string topico)
        {
            try
            {
                var topicFilter = new MqttTopicFilterBuilder()
                                        .WithTopic(topico)
                                        .Build();

                await _mqttClient.SubscribeAsync(topicFilter);
            }
            catch
            {
                Console.WriteLine("Erro no subscribe");
            }


        }

        public async Task<MqttClientPublishResult> Publish(string topico)
        {
            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();
            try
            {

                var options = new MqttClientOptionsBuilder()
                                .WithClientId(_configuration["MQTT:ClientId"])
                                .WithTcpServer(_configuration["MQTT:Host"], Convert.ToInt32(_configuration["MQTT:Port"]))
                                .WithCredentials(_configuration["MQTT:Username"], _configuration["MQTT:Password"])
                                .Build();


                var message = new MqttApplicationMessageBuilder()
                            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                            .WithTopic(topico)
                            .WithPayload("ola api")
                            .Build();

                await client.ConnectAsync(options, CancellationToken.None);

                return await client.PublishAsync(message, CancellationToken.None);
            }
            finally
            {
                await client.DisconnectAsync();
            }

        }
        private void SalvaDados(MqttApplicationMessageReceivedEventArgs args)
        {
            Console.WriteLine($"{args.ApplicationMessage.Topic}: {Encoding.UTF8.GetString(args.ApplicationMessage.Payload)}");
            var topic = args.ApplicationMessage.Topic;
            double valor = Convert.ToDouble(Encoding.UTF8.GetString(args.ApplicationMessage.Payload));
            if (topic == "Distancia")
            {
                _database.PostMedicaoSensorNivel(valor);
            }
            else if(topic == "VazaoEntrada")
            {
                _database.PostMedicaoSensorVazaoEntrada(valor);
            }
        }
    }
}
