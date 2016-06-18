package com.example.takephoto;

import android.os.AsyncTask;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.FileEntity;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Post extends AsyncTask<File, String, File> {

        @Override
        protected File doInBackground(File... params) {
            HttpClient httpclient = new DefaultHttpClient();
            HttpPost httppost = new HttpPost("http://controlprinter.apphb.com/Home/IndexPost");

            try {
                StringEntity se = new StringEntity("Hello", HTTP.UTF_8);//"image/jpeg"
                se.setContentType("text");
                httppost.setEntity(se);

                HttpResponse httpresponse = httpclient.execute(httppost);
                HttpEntity resEntity = httpresponse.getEntity();
                String responseString = EntityUtils.toString(resEntity);
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }
    }
