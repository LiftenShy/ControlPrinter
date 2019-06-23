using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ControlPrinter.Data;
using ControlPrinter.Service.Abstract;
using MessageBus.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace ControlPrinter.Service.Implement
{
    public class ImageService : IImageService
    {
        private readonly IStorageService _storageService;

        public ImageService(IStorageService storageService)
        {
            _storageService = storageService;
        }


        public void ReceiveImage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new Subscription(channel, "ControlPrinter_Image", false);
                while(true)
                {
                    BasicDeliverEventArgs basicDeliveryEventArgs =
                        consumer.Next();

                    ImageServiceModel imageServiceModel;
                    var bf = new BinaryFormatter();
                    using (var ms = new MemoryStream(basicDeliveryEventArgs.Body))
                    {
                        imageServiceModel = (ImageServiceModel)bf.Deserialize(ms);
                    }

                    var image = new Image
                    {
                        OriginalImagePath = imageServiceModel.OriginalImagePath,
                        ProcessedImagePath = imageServiceModel.ProcessedImagePath,
                        ResultImagePath = imageServiceModel.ResultImagePath,
                        CreateDate = DateTime.Now
                    };

                    _storageService.SavePictures(image);

                    consumer.Ack(basicDeliveryEventArgs);
                }
            }
        }
    }
}