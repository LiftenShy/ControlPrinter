using System;
using System.Text;
using ControlPrinter.Service.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ControlPrinter.Service.Implement
{
    public class ImageService : IImageService
    {
        public string Hostname { get; }

        public ImageService(string hostname)
        {
            Hostname = hostname;
        }


        public void ReceiveImage()
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    var body = ea.Body;

                    var message = Encoding.UTF8.GetString(body);

                    channel?.BasicAck(ea.DeliveryTag, false);
                };
                String consumerTag = channel.BasicConsume("ControlPrinter_Image", false, consumer);
            }
        }
    }
}
