package com.darryn.emmclientstub;

import android.content.pm.PackageManager;
import android.content.pm.Signature;
import android.os.Trace;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        try {
            Signature[] sigs =
                    new Signature[0];
            sigs = getApplicationContext().getPackageManager().getPackageInfo(getApplicationContext().getPackageName(), PackageManager.GET_SIGNATURES).signatures;
            for (Signature sig : sigs)
            {
                Log.i("MyApp", "Signature hashcode (EMM Client): " + sig.toCharsString());
            }
            Signature[] sigs2 =
                    new Signature[0];
            sigs2 = getApplicationContext().getPackageManager().getPackageInfo("com.company.SecurityManagers", PackageManager.GET_SIGNATURES).signatures;
            for (Signature sig : sigs2)
            {
                Log.i("MyApp", "Signature hashcode (Security Managers): " + sig.toCharsString());
            }
        } catch (PackageManager.NameNotFoundException e) {
            e.printStackTrace();
        }

    }
}
