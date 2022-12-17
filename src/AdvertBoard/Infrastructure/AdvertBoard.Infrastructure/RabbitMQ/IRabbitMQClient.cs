using RabbitMQ.Client;

namespace AdvertBoard.Infrastructure.RabbitMQ
{
    public interface IRabbitMQClient
    {
        Task<List<string>> receive();
        Task send(string message);
    }
}