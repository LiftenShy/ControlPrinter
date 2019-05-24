using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using OpenCvSharp;
using RabbitMQ.Client;

namespace ScreenshotService
{
    class Program
    {
        private static readonly string QUEUENAME = "ControlPrinter_Image";

        static void Main(string[] args)
        {
            //Take photo
            //IPictureService picture = new PictureService();
            //var fileName = picture.TakePicture();
            
            //Process photo

            //Mat src = new Mat(@"./10f93c15-3b5d-4449-af0a-ec8e3b0ef655.png", ImreadModes.Grayscale);
            //Mat src2 = new Mat(@"./1.png", ImreadModes.Grayscale);
            //Mat dst = new Mat();
            //Mat srcDst = new Mat();

            //Cv2.Canny(src, dst, 50, 200);
            //Cv2.Canny(src2, srcDst, 50, 200);

            //Mat result = new Mat();

            //Cv2.Subtract(dst, srcDst, result);

            /*using (new Window("srcDst image", srcDst))
            using (new Window("dst image", dst))
            using (new Window("result image", result))
            {
                Cv2.WaitKey();
            }*/

            //Stream stream = new MemoryStream(dst.ToBytes());

            //Send photo
            //SendPicture(Image.FromStream(stream));
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
