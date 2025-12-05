using PaymentsService.Application.Interface;
using System.Text.Json;

namespace PaymentsService.Infraestructure.Service
{
    public class KafkaProducer : IEventProducer
    {
        public Task PublishAsync(string topic, object data)
        {
            Console.WriteLine($"[EVENT PUBLISHED] Topic: {topic} Data: {JsonSerializer.Serialize(data)}");
            return Task.CompletedTask;
        }
    }
}
