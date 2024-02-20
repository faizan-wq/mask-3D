using com.adjust.sdk;
using Firebase.Analytics;
using GameAnalyticsSDK;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD;
public class AnalyticsManagerProgression : MonoBehaviour
{
    public static AnalyticsManagerProgression instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    #region Events

    public void ProgressionEvent(int Id, int LevelNumber)
    {
      //  FB_ProgressionEvent(Id, LevelNumber);
        GA_ProgressionEvent(Id, LevelNumber);
    }
    public void VideoEvent(string placement)
    {
       // FB_VideoEvent(placement);
        GA_VideoEvent(placement);
    }
    public void InterstitialEvent(string placement)
    {
       // FB_InterstitialEvent(placement);
        GA_InterstitialEvent(placement);
    }
    public void CustomScreenEvent(string placement)
    {
       // FB_CustomScreenEvent(placement);
        GA_CustomScreenEvent(placement);
    }
    public void CustomBtnEvent(string placement)
    {
       // FB_CustomBtnEvent(placement);
        GA_CustomBtnEvent(placement);
    }
    public void IAPEvent(string sku)
    {
       // FB_IAPEvent(sku);
        GA_IAPEvent(sku);
    }

    #endregion


    #region firebaseEvents

    public void FB_ProgressionEvent(int Id, int LevelNumber)
    {
        if (GD.Controller.Instance.firebaseInitialized)
        {
            string msg = "";
            if (Id == 1)//levelStart
                msg = "GD_LVL_Start";
            else if (Id == 2)//levelWin
                msg = "GD_LVL_Complete";
            else if (Id == 3)//levelFail
                msg = "GD_LVL_Fail";
            else if (Id == 4)//levelTie
                msg = "GD_LVL_Tie";
            else if (Id == 5)//levelRetry
                msg = "GD_LVL_Retry";
            FirebaseAnalytics.LogEvent(msg + LevelNumber);
            print("FB_ProgressionEvent :" + msg + LevelNumber);
        }
    }

    public void FB_VideoEvent(string placement)
    {
        FirebaseAnalytics.LogEvent("ADS_REWARDED_" + placement);
        print("FB_ADS_REWARDED_" + placement);
    }
    public void FB_InterstitialEvent(string placement)
    {
        FirebaseAnalytics.LogEvent("ADS_INTER_" + placement);
        print("FB_ADS_INTER_" + placement);
    }
    public void FB_CustomScreenEvent(string placement)
    {
        FirebaseAnalytics.LogEvent("GD_SCREEN_" + placement);
        print("FB_GD_SCREEN_" + placement);
    }
    public void FB_CustomBtnEvent(string placement)
    {
        FirebaseAnalytics.LogEvent("GD_BTN_" + placement);
        print("FB_GD_BTN_" + placement);
    }
    public void FB_IAPEvent(string sku)
    {
        FirebaseAnalytics.LogEvent("IAP_" + sku);
        print("FB_IAP_" + sku);
    }
    #endregion




    #region GameAnalyticsEvent
    public void GA_ProgressionEvent(int Id, int LevelNumber)
    {

        string msg = "";

        if (Id == 1)//levelStart
            msg = "GD_LVL_Start";
        else if (Id == 2)//levelWin
            msg = "GD_LVL_Complete";
        else if (Id == 3)//levelFail
            msg = "GD_LVL_Fail";
        else if (Id == 4)//levelTie
            msg = "GD_LVL_Tie";
        else if (Id == 5)//levelRetry
            msg = "GD_LVL_Retry";

        GameAnalytics.NewProgressionEvent((GAProgressionStatus)Id, LevelNumber.ToString());
        GameAnalytics.NewDesignEvent(msg + LevelNumber);
        print("GA_ProgressionEvent :" + msg + LevelNumber);
    }
    public void GA_CustomScreenEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("GD_SCREEN_" + placement);
        print("GA_GD_SCREEN_" + placement);
    }
    public void GA_CustomBtnEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("GD_BTN_" + placement);
        print("GA_GD_BTN_" + placement);
    }
    public void GA_VideoEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("ADS_REWARDED_" + placement);
        print("GA_ADS_REWARDED_" + placement);
    }
    public void GA_InterstitialEvent(string placement)
    {
        GameAnalytics.NewDesignEvent("ADS_INTER_" + placement);
        print("GA_ADS_INTER_" + placement);
    }
    public void GA_IAPEvent(string sku)
    {
        GameAnalytics.NewDesignEvent("IAP_" + sku);
        print("GA_IAP_" + sku);
    }
    #endregion


    
}
