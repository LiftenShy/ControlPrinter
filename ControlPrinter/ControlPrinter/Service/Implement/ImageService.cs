using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
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
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("ControlPrinter_Image", true, false, false, null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                var tag = channel.BasicConsume("ControlPrinter_Image", true, consumer);
                consumer.Received += async (o, a) =>
                {
                    ImageServiceModel imageServiceModel;
                    var bf = new BinaryFormatter();
                    using (var ms = new MemoryStream(a.Body))
                    {
                        imageServiceModel = (ImageServiceModel)bf.Deserialize(ms);
                    }

                    var image = new Image
                    {
                        OriginalImagePath = SaveImgaeToFolder(imageServiceModel.OriginalImagePath),
                        ProcessedImagePath = SaveImgaeToFolder(imageServiceModel.ProcessedImagePath),
                        ResultImagePath = SaveImgaeToFolder(imageServiceModel.ResultImagePath),
                        CreateDate = DateTime.Now
                    };

                    _storageService.SavePictures(image);

                    await Task.Yield();
                };

            }
        }

        private string SaveImgaeToFolder(string path)
        {
            string fileName = Guid.NewGuid() + ".png";
            string newSavePath = Path.GetFullPath($@".\\wwwroot\Images\{fileName}");
            File.Copy(path, newSavePath);

            return $@"\Images\{fileName}";
        }
    }
}