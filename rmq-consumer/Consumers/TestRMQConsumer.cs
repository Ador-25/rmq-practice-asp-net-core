using RabbitMQ.Client;
using System.Text;

namespace rmq_consumer.BackgroundServices
{
    public class TestRMQConsumer : BackgroundService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        public TestRMQConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory() { HostName = _configuration["RMQ_Host"], UserName = _configuration["RMQ_User"], Password = _configuration["RMQ_Pass"] };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "ador-test-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Simulate some background processing
                await Task.Delay(5000, stoppingToken);

                // Publish a sample message to the queue


            }
        }
    }
}
