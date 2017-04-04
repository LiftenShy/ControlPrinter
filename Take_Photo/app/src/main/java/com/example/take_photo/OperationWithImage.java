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
}
