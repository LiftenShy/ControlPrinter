package com.example.take_photo;

import android.graphics.Bitmap;
import android.graphics.Color;
import android.util.Log;
import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;

public class OperationWithImage {

    static void sendImage(byte[] image) throws IOException {

        String boundary = "*****";
        String attachmentName = "fileBase";
        String attachmentFileName = "pic.png";
        String crlf = "\r\n";
        String twoHyphens = "--";

        HttpURLConnection conn = null;
        try {
            URL url = new URL("http://controlprinter.apphb.com/Home/HomePagePost");
            conn = (HttpURLConnection) url.openConnection();
            conn.setUseCaches(false);
            conn.setDoOutput(true);

            conn.setRequestMethod("POST");
            conn.setRequestProperty("Connection", "Keep-Alive");
            conn.setRequestProperty("Cache-Control", "no-cache");
            conn.setRequestProperty(
                    "Content-Type", "multipart/form-data;boundary=" + boundary);

            DataOutputStream request = new DataOutputStream(
                    conn.getOutputStream());

            request.writeBytes(twoHyphens + boundary + crlf);
            request.writeBytes("Content-Disposition: form-data; name=\"" +
                    attachmentName + "\";filename=\"" +
                    attachmentFileName + "\"" + crlf);
            request.writeBytes(crlf);
            request.write(image);
            request.writeBytes(crlf);
            request.writeBytes(twoHyphens + boundary +
                    twoHyphens + crlf);

            request.flush();
            request.close();

            InputStream responseStream = new
                    BufferedInputStream(conn.getInputStream());
            BufferedReader responseStreamReader =
                    new BufferedReader(new InputStreamReader(responseStream));
            String line = "";
            StringBuilder stringBuilder = new StringBuilder();

            while ((line = responseStreamReader.readLine()) != null) {
                stringBuilder.append(line).append("\n");
            }
            responseStreamReader.close();
            String response = stringBuilder.toString();

            responseStream.close();
            conn.disconnect();


            int serverResponseCode = conn.getResponseCode();
            String serverResponseMessage = conn.getResponseMessage();
            Log.i("uploadFile", "HTTP Response is : "
                    + serverResponseMessage + ": " + serverResponseCode);
            if (serverResponseCode == 200) {
                Log.d("SendData", "All okay");
            }
            if (serverResponseCode == 500) {
                Log.d("SendData", "Not sucsses send");
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (ProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /*public static Bitmap ConvertToBinaryImage(Bitmap image, int threshold) {

        int width = image.getWidth();
        int height = image.getHeight();
        int size = width * height;

        //Получаем матрицу пикселей изображения
        int[] pixels = new int[size];
        image.getPixels(pixels, 0, width, 0, 0, width, height);
        image.recycle();

        //Проходим по всем пикселям в матрице, выполняя перевод в полутоновое изображение и бинаризацию по порогу
        for (int i = 0; i < size; i++) {
            int color = pixels[i];
            int r = Color.red(color);
            int g = Color.green(color);
            int b = Color.blue(color);
            double luminance = (0.299 * r + 0.0f + 0.587 * g + 0.0f + 0.114 * b + 0.0f);
            pixels[i] = luminance > threshold ? Color.WHITE : Color.BLACK;
        }
        return Bitmap.createBitmap(pixels,width,height,image.getConfig());
    }

    public static Bitmap FilterLaplasiana(Bitmap image) {
        Bitmap bitmap = image.copy(image.getConfig(),true);
        int color, r, g, b;
        int summRGB=0;//, summG=0, summB=0;

        /*int[,] mask = new int[3, 3] { { 1, 1, 1 },
            { 1, -8, 1 },
            { 1, 1, 1 } };

        //int[,] mask = new int[3, 3] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
        //int[,] mask = new int[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
        Bitmap old = new Bitmap(bmp);
        for(int x = 1; x < bmp.Size.Width - 1; x++)
        {
            for(int y = 1; y < bmp.Size.Height - 1; y++)
            {
                int tmp = 0;
                int q = 0;
                for(int i = x - 1; i <= x+1; i++)
                {
                    int w = 0;
                    for(int j = y -1; j <= y+1; j++)
                    {
                        tmp = tmp + old.GetPixel(i, j).R * mask[q, w];
                        w++;
                    }
                    q++;
                }
                int newColor =  tmp;

                if (newColor > 255)
                {
                    newColor = 255;
                    //MessageBox.Show("255");
                }
                else
                {
                    if (newColor < 0)
                    {
                        newColor = 0;
                        //MessageBox.Show("0");
                    }
                }
                Color color = Color.FromArgb(newColor, newColor, newColor);
                bmp.SetPixel(x, y, color);

        return bitmap;
    }*/
}
