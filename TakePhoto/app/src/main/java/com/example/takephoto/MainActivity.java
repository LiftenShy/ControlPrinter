package com.example.takephoto;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.View;

import java.io.File;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import java.io.ByteArrayOutputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.UnknownHostException;
import java.util.HashMap;
import java.util.Map;

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
            //"http://controlprinter.apphb.com/Home/android"

            final File f = new File("D:\\Diplom\\TakePhoto\\app\\src\\main\\res\\drawable\\pic.jpg");
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
            RequestQueue MyRequestQueue = Volley.newRequestQueue(this);
            String url = "http://controlprinter.apphb.com/Home/IndexPost";
            StringRequest MyStringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    //This code is executed if the server responds, whether or not the response contains data.
                    //The String 'response' contains the server's response.
                }
            }, new Response.ErrorListener() { //Create an error listener to handle errors appropriately.
                @Override
                public void onErrorResponse(VolleyError error) {
                    //This code is executed if there is an error.
                }
            }) {
                @Override
                protected Map<String, String> getParams() {
                    Map<String,String> MyData = new HashMap<String,String>();
                    MyData.put(f.getName(), String.valueOf(f)); //Add the data you'd like to send to the server.
                    return MyData;
                }
            };
            MyRequestQueue.add(MyStringRequest);
        }
    }

    public void TakePhoto_Click(View v) throws UnknownHostException {
        Intent cameraintent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        startActivityForResult(cameraintent, CAM_REQUEST);
    }
}
