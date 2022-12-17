using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;

namespace AdvertBoard.Infrastructure.RabbitMQ
{
    public class RabbitMQClient : IRabbitMQClient
    {
        private IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "mjjowevf",
                Password = "nR9WFQJnuDy7yN3I4HmSUqYyCuBSa5G_",
                VirtualHost = "mjjowevf",
                HostName = "sparrow-01.rmq.cloudamqp.com"
            };

            return factory.CreateConnection();
        }
        public async Task send(string message)
        {
            try
            {
                var connection = GetConnection();
                var channel = connection.CreateModel();
                /*string message = DateTime.UtcNow.ToString();*/

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "SolarLab",
                                     routingKey: "score",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"Отправлено {message}");

            }
            catch (Exception)
            {


            }

        }
        public async Task<List<string>> receive()
        {
            try
            {
                var connection = GetConnection();
                var channel = connection.CreateModel();

                var consumer = new EventingBasicConsumer(channel);
                var messages = new List<string>();

                channel.BasicConsume(queue: "scores",
                                    autoAck: true,
                                    consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    messages.Add(message);
                    Console.WriteLine($"Получено {message}");
                };

                return messages;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

    }
}
