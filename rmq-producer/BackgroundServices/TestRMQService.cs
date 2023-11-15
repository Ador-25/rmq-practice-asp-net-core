using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using rmq_producer.Models;
using System.Text;


namespace rmq_producer.BackgroundServices
{
    public class TestRMQService : BackgroundService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        public TestRMQService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory() { HostName = _configuration["RMQ_Host"], UserName = _configuration["RMQ_User"], Password = _configuration["RMQ_Pass"] };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "ador-test-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        }
        public void SendMessage(Order order)
        {
            string message = JsonConvert.SerializeObject(order);
            var body = Encoding.UTF8.GetBytes(message);

            // Publish message to the queue
            _channel.BasicPublish(exchange: "", routingKey: "ador-test-queue", basicProperties: null, body: body);
            Console.WriteLine($" [x] Sent \n {message}");
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Simulate some background processing
                await Task.Delay(5000, stoppingToken);

                // Publish a sample message to the queue

                SendMessage(new Order { Id = new Random().Next(10), Description = "Test string", Amount = 50.00, IsCompleted = new Random().Next(10) % 2 == 0 });
            }
        }
    }
}
