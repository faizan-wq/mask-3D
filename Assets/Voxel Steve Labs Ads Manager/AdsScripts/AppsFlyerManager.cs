using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AppsFlyerSDK;

public class AppsFlyerManager : MonoBehaviour
{
    public static AppsFlyerManager Instance;
    private void OnEnable()
    {
        if (!Instance)
            Instance = this;
    }
    public void AdImperssionEevent(string Adtype)
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        //eventValues.Add("ADs_Impression", event_name);
        // eventValues.Add("Show Ads", "show_ads");

        //AppsFlyer.sendEvent(Adtype, eventValues);
        //AppsFlyer.sendEvent("ads_impression_count", eventValues);
        Debug.Log(Adtype);
    }
    public void interstitialAdsEvent()
    {
        int count = PlayerPrefs.GetInt("InterstitialAdsCount", 0) + PlayerPrefs.GetInt("RewardedAdsCount", 0);
        if (count > 1 && count < 16 && count % 2 == 1)
        {
            //AppsFlyer.sendEvent("af_rv_inter_watched_" + count + "_times", null);
        }
        Debug.Log("af_rv_inter_watched_" + count);
    }
    public void rewardedAdsEvent()
    {
        int count = PlayerPrefs.GetInt("InterstitialAdsCount", 0) + PlayerPrefs.GetInt("RewardedAdsCount", 0);
        if (count > 1 && count < 16 && count % 2 == 1)
        {
            //AppsFlyer.sendEvent("af_rv_inter_watched_" + count + "_times", null);
        }
        Debug.Log("af_rv_inter_watched_" + count);
    }

    public void AdImperssionEvent(string Adtype)
    {   
        //AppsFlyer.sendEvent(Adtype);
        Debug.Log(Adtype);
    }
}