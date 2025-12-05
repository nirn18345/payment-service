namespace PaymentsService.Application.Interface
{
    public interface IEventProducer
    {
        Task PublishAsync(string topic, object data);
    }
}
