package com.example.takephoto;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.View;
import android.widget.Toast;

import java.io.File;

import java.io.ByteArrayOutputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.UnknownHostException;


public class MainActivity extends Activity {

    private static final int CAM_REQUEST = 1313;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == CAM_REQUEST) {
            //"http://controlprinter.apphb.com/Home/IndexPost"

            final File f = new File(getFilesDir(), "pic.jpg");
            //Convert bitmap to byte array
            ByteArrayOutputStream byteStream = new ByteArrayOutputStream();
            Bitmap bmp = (Bitmap) data.getExtras().get("data");
            bmp.compress(Bitmap.CompressFormat.JPEG, 0, byteStream);
            byte[] bitmapdata = byteStream.toByteArray();

            //write the bytes in file
            FileOutputStream fos = null;
            try {
                f.createNewFile();
                fos = new FileOutputStream(f);
                fos.write(bitmapdata);
                fos.flush();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            Post post = new Post();
            post.doInBackground(f);
            /*try {
            URL url = new URL("http://controlprinter.apphb.com/Home/IndexPost");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setReadTimeout(10000);
            conn.setConnectTimeout(15000 );
            conn.setRequestMethod("GET");
            //conn.setRequestProperty("Content-Type", "application/string");
            conn.setDoOutput(true);
            conn.connect();
        } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }*/

        }
    }

    public void TakePhoto_Click(View v) throws UnknownHostException {
        Intent cameraintent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        startActivityForResult(cameraintent, CAM_REQUEST);
    }
}