using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using OpenCvSharp;
using RabbitMQ.Client;
using ScreenshotService.Services.Abstraction;
using ScreenshotService.Services.Implements;

namespace ScreenshotService
{
    class Program
    {
        private static readonly string QUEUENAME = "ControlPrinter_Image";

        static void Main(string[] args)
        {
            //Take photo
            IPictureService picture = new PictureService();
            var fileName = picture.TakePicture();
            
            //Process photo

            Mat src = new Mat(fileName, ImreadModes.Grayscale);
            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);
            using (new Window("src image", src))
            using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }

            Stream stream = new MemoryStream(dst.ToBytes());

            //Send photo
            SendPicture(Image.FromStream(stream));
        }

        static void SendPicture(Image image)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("CP", ExchangeType.Direct);
                channel.QueueDeclare(QUEUENAME, false, false, false, null);
                channel.QueueBind(QUEUENAME, "CP", "", null);

                var bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, image);
                    
                    channel.BasicPublish("CP", "", null, ms.ToArray());
                }
                
            }
        }
    }
}
