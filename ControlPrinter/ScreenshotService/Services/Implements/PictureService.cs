using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using OpenCvSharp;
using ScreenshotService.Services.Abstraction;

namespace ScreenshotService.Services.Implements
{
    public class PictureService : IPictureService
    {
        VideoCapture _capture;
        Mat _frame;
        private Thread _camera;

        public string TakePicture()
        {
            try
            {
                _camera = new Thread(() => { });
                _camera.Start();

                _frame = new Mat();
                _capture = new VideoCapture(0);
                _capture.Open(0);

                _capture.Read(_frame);

                Stream stream = new MemoryStream(_frame.ToBytes());

                var snapshot = new Bitmap(stream);

                var fileName = Guid.NewGuid().ToString() + ".png";

                snapshot.Save($@".\{fileName}", ImageFormat.Png);

                _capture.Release();
                _camera.Abort();

                return fileName;
            }
            catch (Exception e)
            {
                _capture.Release();
                _camera.Abort();
                throw new Exception($"Exception {e.Message}");
            }
        }
    }
}
