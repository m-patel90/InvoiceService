using Invoice.Applicaion.CQRS.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Invoice.Applicaion.CQRS.Handlers
{
    public class EmailHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly ILogger<EmailHandler> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channle;
        public EmailHandler(ILogger<EmailHandler> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channle = _connection.CreateModel();
            _channle.QueueDeclare(queue: "myqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        public Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notification.product));
            _channle.BasicPublish(exchange: "", routingKey:"myqueue", basicProperties: null, body: body);

            _logger.LogInformation("Email sent");
            return Task.CompletedTask;
        }
    }
}
