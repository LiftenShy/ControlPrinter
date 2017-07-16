using System;
using System.Threading.Tasks;
using CP.Business.Abstract;
using CP.Data.ModelTransport;
using MassTransit;

namespace CP.Business
{
    public class QueueService : IQueueService
    {
        IBusControl bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
        {
            var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        });
        public QueueService()
        { 
            bus.Start();
        }
        public async Task Send(Uri uri, TransportImageModel image)
        {
            ISendEndpoint client = await bus.GetSendEndpoint(uri);
            await client.Send(image);
        }
    }
}
