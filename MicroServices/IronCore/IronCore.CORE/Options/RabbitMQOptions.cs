namespace IronCore.CORE.Options;
public class RabbitMQOptions
{
    public string Uri { get; set; }
    public IEnumerable<RabbitMQQueueOptions> Queues { get; set; }
}