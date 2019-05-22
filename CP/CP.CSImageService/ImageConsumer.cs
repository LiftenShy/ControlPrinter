
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using CP.Business.Abstract;
using CP.CSImageService.Service.Abstract;
using CP.Data.Models;
using CP.Data.ModelTransport;
using CP.Storage;
using MassTransit;
using Serilog;
using Unity;

namespace CP.CSImageService
{
    class ImageConsumer : IConsumer<TransportImageModel>
    {
        [Dependency]
        public IImageProcessesService ImageProcessesService { get; set; }

        [Dependency]
        public IImageService ImageService { get; set; }

        [Dependency]
        public IFileManager FileManager { get; set; }

        public Task Consume(ConsumeContext<TransportImageModel> context)
        {
            Stopwatch time = new Stopwatch();
            Log.Information("Start processes image");
            time.Start();
            long start = time.ElapsedMilliseconds;
            Bitmap incomeImg;
            using (MemoryStream memoryStream = new MemoryStream(context.Message.ImageBytes))
            {
                incomeImg = new Bitmap(memoryStream);
                incomeImg = ImageProcessesService.SetBinaryImage(incomeImg, 128);
                incomeImg = ImageProcessesService.SobelFilter(incomeImg);
            }
            using (MemoryStream mStream = new MemoryStream())
            {
                incomeImg.Save(mStream, ImageFormat.Png);
                mStream.Position = 0;
                ImageService.SaveImage(new Data.Models.Image {DateLoad = DateTime.Now}, mStream);
            }
            Bitmap staticImg;
            using (MemoryStream memoryStreamStatic = new MemoryStream(
                        FileManager.GetFile(
                            ImageService.GetImageUri(
                                ImageService.GetStaticImage().NameImage))))
            {
                staticImg = new Bitmap(memoryStreamStatic);
            }
            staticImg = ImageProcessesService.SetBinaryImage(staticImg, 128);
            staticImg = ImageProcessesService.SobelFilter(staticImg);

            using (MemoryStream resultMemoryStream = new MemoryStream())
            {
                Bitmap resultBitmap = ImageProcessesService.CompareImages(staticImg, incomeImg);
                resultBitmap.Save(resultMemoryStream, ImageFormat.Png);
                resultMemoryStream.Position = 0;
                ImageService.SaveResultImage(new ResultImg {DateLoad = DateTime.Now}, resultMemoryStream);
            }
            time.Stop();
            long finish = time.ElapsedMilliseconds;
            Log.Information("Finish processes image");
            Log.Debug($"All elapsed time: {TimeSpan.FromSeconds(finish - start)}");
            return Task.FromResult(0);
        }
    }
}
