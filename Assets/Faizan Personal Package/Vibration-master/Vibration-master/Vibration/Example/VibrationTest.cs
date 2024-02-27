using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VibrationTest : MonoBehaviour
{
   
    public string inputValue;

    // Use this for initialization
    void Start ()
    {
        Vibration.Init ();
        Debug.Log ( "Application.isMobilePlatform: " + Application.isMobilePlatform );
        //txtAndroidVersion.text = "Android Version: " + Vibration.AndroidVersion.ToString ();
    }

    // Update is called once per frame
    void Update ()
    {

    }

  
    public void TapVibratePattern ()
    {
        string[] patterns = inputValue.Replace ( " ", "" ).Split ( ',' );
        long[] longs = Array.ConvertAll<string, long> ( patterns, long.Parse );

        Debug.Log ( longs.Length );
        //Vibration.Vibrate ( longs, int.Parse ( inputRepeat.text ) );
#if UNITY_ANDROID
        Vibration.VibrateAndroid ( longs, -1 );
#endif
    }


   
}
