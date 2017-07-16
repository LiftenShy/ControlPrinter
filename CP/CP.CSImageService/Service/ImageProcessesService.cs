
using System.Drawing;
using CP.CSImageService.Service.Abstract;

namespace CP.CSImageService.Service
{
    class ImageProcessesService : IImageProcessesService
    {
        public Bitmap SetBinaryImage(Bitmap image, double threshold)
        {
            Color color = new Color();
            double luminance;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    color = image.GetPixel(x, y);
                    luminance = (color.R + color.G + color.B) / 3;
                    image.SetPixel(x, y, luminance > threshold ? Color.White : Color.Black);
                }
            }
            return image;
        }

        public Bitmap SobelFilter(Bitmap image)
        {

            Bitmap b = new Bitmap(image);
            Bitmap bb = new Bitmap(image);
            int width = b.Width;
            int height = b.Height;
            int[,] gx = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            int limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = b.GetPixel(i, j).R;
                    allPixG[i, j] = b.GetPixel(i, j).G;
                    allPixB[i, j] = b.GetPixel(i, j).B;
                }
            }

            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {

                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;
                    rc = 0;
                    gc = 0;
                    bc = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit ||
                        new_bx * new_bx + new_by * new_by > limit)
                        bb.SetPixel(i, j, Color.Black);
                    else
                        bb.SetPixel(i, j, Color.White);
                }
            }
            return bb;
        }

        public Bitmap CompareImages(Bitmap firstImage, Bitmap secondImage)
        {
            int widht = firstImage.Width > secondImage.Width
                ? widht = secondImage.Width
                : widht = firstImage.Width;
            int height = firstImage.Height > secondImage.Height
                ? height = secondImage.Height
                : height = firstImage.Height;

            var resultImage = new Bitmap(widht, height);

            Color colorFirstImage;
            Color colorSecondImage;
            int r, b, g;

            for (int x = 0; x < widht; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    colorFirstImage = firstImage.GetPixel(x, y);
                    colorSecondImage = secondImage.GetPixel(x, y);
                    r = colorFirstImage.R - colorSecondImage.R;
                    b = colorFirstImage.B - colorSecondImage.B;
                    g = colorFirstImage.G - colorSecondImage.G;
                    if (r == 255 || b == 255 || g == 255)
                    {
                        resultImage.SetPixel(x, y, Color.Red);
                    }
                    else if (r == -255 || b == -255 || g == -255)
                    {
                        resultImage.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, Color.Black);
                    }

                }
            }
            return resultImage;
        }

    }
}
