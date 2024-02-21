using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public enum AdStatus { 
   FirstCall,
    Loaded,
    Progress,
    Failed,
}
public class Native_AdsController : MonoBehaviour
{
  
    private bool unifiedNativeAdLoaded;
    // private UnifiedNativeAd nativeAd;
    // private UnifiedNativeAd newNativeAd;
    public RawImage iconTexture;
    public RawImage mainImage;
    public Text headline;
    public GameObject installButton;
    public Text text_btn;
    public RawImage adChoice;
    public Text mainBodyText;
    private AdStatus adStatus;
    // Use this for initialization
    public static string nativeAdsID= "ca-app-pub-4164748869637577/2539651207";
    List<GameObject> imageObjs = new List<GameObject>();
    public GameObject nativePanel;

    // Update is called once per frame
    public void Awake()
    {
        adStatus = AdStatus.FirstCall;
       
    }
    private void OnEnable()
    {
        try
        {

     
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            nativePanel.SetActive(false);
            switch (adStatus)
            {
                case AdStatus.FirstCall:
                    RequestNativeAd();
                 
                    break;
                case AdStatus.Progress:
              
                    break;
                case AdStatus.Failed:
                    RequestNativeAd();
                
                    break;
                case AdStatus.Loaded:
                 
                    RequestNativeAd();
                    break;

            }

        }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);

        }
    }

   

    public  void RequestNativeAd()
    {

        // if (nativeAd != null)
        // {
        //     nativeAd.Destroy();
        // }
        // adStatus = AdStatus.Progress;
        // AdLoader adLoader = new AdLoader.Builder(nativeAdsID)
        //     .ForUnifiedNativeAd()
        //     .Build();
        // adLoader.OnUnifiedNativeAdLoaded += HandleUnifiedNativeAdLoaded;
        // adLoader.OnAdFailedToLoad += HandleNativeAdFailedToLoad;
        // adLoader.LoadAd(new AdRequest.Builder().Build());
  

    }
   


}
