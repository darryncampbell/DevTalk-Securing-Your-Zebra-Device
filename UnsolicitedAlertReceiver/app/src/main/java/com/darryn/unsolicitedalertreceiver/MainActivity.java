package com.darryn.unsolicitedalertreceiver;

import android.content.Intent;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import java.util.concurrent.ThreadLocalRandom;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Intent launchIntent = getIntent();
        if (launchIntent != null && launchIntent.hasExtra("AlertMessage"))
        {
            String AlertMessage = launchIntent.getStringExtra("AlertMessage");
            Log.i("Unsolicited Alert", AlertMessage);
            Toast.makeText(this, AlertMessage, Toast.LENGTH_LONG).show();
            TextView ui = (TextView)findViewById(R.id.txtUi);
            int randomNum = 0;
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
                randomNum = ThreadLocalRandom.current().nextInt(0, 100 + 1);
            }
            ui.setText(AlertMessage + " " + randomNum);
        }
    }

}
