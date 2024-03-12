using System;
using System.Collections.Generic;
using com.adjust.sdk;
using GD;
using ToastPlugin;
using UnityEngine;
using UnityEngine.UI;


public class AdsManager : MonoBehaviour
{
    public string MaxSdkKey = "6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR";
    public string InterstitialAdUnitId = "0bf5dd259a7babe3";
    public string RewardedAdUnitId = "5d75002bbc4126b9";
    public string AppOpenAdUnitId = "ENTER_AppOpen_AD_UNIT_ID_HERE";
    public string BannerAdUnitId = "ENTER_BANNER_AD_UNIT_ID_HERE";
    public string MRecAdUnitId = "ENTER_MREC_AD_UNIT_ID_HERE";
    public bool IsRewardedLoaded = false;
    public bool _isBannerReady = false;
    private bool isBannerShowing;
    private bool isMRecShowing;
    private bool isRewardedIntestitial;
    public static bool isInterstialAdPresent;
    private int interstitialRetryAttempt;
    private int rewardedRetryAttempt;
    public MaxSdkBase.BannerPosition maxBannerPosition;
    public static AdsManager Instance;
    public static bool isMaxBannerInitialized = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public void Initializer()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            // AppLovin SDK is initialized, configure and start loading ads.
            Debug.Log("MAX SDK Initialized");
            if (!GD.Controller.stopAds)
                GD.Controller.allowFirebaseAds = true;
            InitializeInterstitialAds();
            InitializeRewardedAds();

            InitializeBannerAds();




          
        };

        if (!GD.Controller.stopAds)
        {
            MaxSdk.SetSdkKey(MaxSdkKey);
            MaxSdk.InitializeSdk();
        }

    }



    #region Interstitial Ad Methods

    private void InitializeInterstitialAds()
    {

        if (!GlobalConstant.IsInterstitialAd || !GlobalConstant.IsInterstitialMaxAd)
            return;
        Debug.Log("Interstetial Ad is Showing");

        // Attach callbacks
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        // Load the first interstitial
        LoadInterstitial();
    }

    public void LoadInterstitial()
    {
        //interstitialStatusText.text = "Loading...";
        if (!MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
            MaxSdk.LoadInterstitial(InterstitialAdUnitId);
        // Debug.Log(InterstitialAdUnitId+ "InterstitialAdUnitId");
    }

    public void ShowInterstitial(bool _isRewardedIntestitial = false)
    {

        if (!GlobalConstant.IsInterstitialAd)
            return;

        Debug.Log("Interstetial Ads are playing");
        isRewardedIntestitial = _isRewardedIntestitial;

        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId) /*&& GlobalConstant.IsInterstitialMaxAd*/)
        {
            isInterstialAdPresent = true;
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            AnalyticsManagerProgression.instance.InterstitialEvent("Max Interstetial Ad Called");
            return;
        }
        else if (AdMob_GF.IsInterstitialReady(AdMob_GF.RequestFloorType.AllPrice) /*&& GlobalConstant.IsInterstitialAdmobAd*/ )
        {


            AdMob_GF.ShowInterstitial();
            AnalyticsManagerProgression.instance.InterstitialEvent("Admob Interstetial Ad Called");
            isInterstialAdPresent = true;



        }

        if (!GlobalConstant.IsInterstitialMaxAd)
        {
            return;
        }
        LoadInterstitial();
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
        // interstitialStatusText.text = "Loaded";
        Debug.Log("Interstitial loaded");

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        Debug.Log(adUnitId + "InterstitialAdUnitId");
        // interstitialStatusText.text = "Failed load: " + errorCode + "\nRetrying in 3s...";
        Debug.Log("Interstitial failed to load with error code: " + errorCode);

        // Interstitial ad failed to load. We recommend retrying with exponentially higher delays.

        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, interstitialRetryAttempt);

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        Debug.Log("Interstitial failed to display with error code: " + errorCode);
        LoadInterstitial();
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        Debug.Log("Interstitial dismissed");
        PlayerPrefs.SetInt("InterstitialAdsCount", PlayerPrefs.GetInt("InterstitialAdsCount", 0) + 1);
        if (isRewardedIntestitial)
        {
            Controller.Instance.ActionVideo(true);
        }

        LoadInterstitial();
    }

    #endregion


    #region Rewarded Ad Methods

    private void InitializeRewardedAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;


        // Load the first RewardedAd
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsRewardedAd)
            return;
        //  rewardedStatusText.text = "Loading...";
        if (!MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
            MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }

    public void ShowRewardedAd()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsRewardedAd)
            return;


        Debug.Log("Rewarded Ad is Showing");
        AnalyticsManagerProgression.instance.VideoEvent("Rewarded ad");
        if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
        {
            isInterstialAdPresent = true;

            MaxSdk.ShowRewardedAd(RewardedAdUnitId, PlayerPrefs.GetString("RewardedAdPlacement", "default"));
        }
        else
        {
            //AdMob_GF.AdmobRewardedShow();
            AdMob_GF.ShowRewardedAdmob();
        }

    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        Debug.Log("Rewarded ad loaded");
        IsRewardedLoaded = true;
        // Reset retry attempt
        rewardedRetryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // rewardedStatusText.text = "Failed load: " + errorCode + "\nRetrying in 3s...";
        Debug.Log("Rewarded ad failed to load with error code: " + errorCode);

        // Rewarded ad failed to load. We recommend retrying with exponentially higher delays.

        rewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, rewardedRetryAttempt);
        IsRewardedLoaded = false;
        Invoke("LoadRewardedAd", (float)retryDelay);

    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        Debug.Log("Rewarded ad failed to display with error code: " + errorCode);
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId)
    {
        Debug.Log("Rewarded ad displayed");
    }

    private void OnRewardedAdClickedEvent(string adUnitId)
    {
        Debug.Log("Rewarded ad clicked");
    }

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        Debug.Log("Rewarded ad dismissed");
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        PlayerPrefs.SetInt("RewardedAdsCount", PlayerPrefs.GetInt("RewardedAdsCount", 0) + 1);
        Controller.Instance.ActionVideo(true);
        Debug.Log("Rewarded ad received reward");
    }

    #endregion

    #region Appopen Ad Methods

    private void InitializeAppOpen()
    {

        if (!GlobalConstant.isAppOpen)
            return;

        MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
        MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        // if (!GlobalConstant.isAppOpen)
        //     return;
        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            //  MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
            AdMob_GF.ShowRewardedAdmob();
        }
    }


    public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }




    #endregion

    #region Banner Ad Methods

    private void InitializeBannerAds()
    {



        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdk.CreateBanner(BannerAdUnitId, maxBannerPosition);

        // Set background or background color for banners to be fully functional.
        MaxSdk.SetBannerExtraParameter(BannerAdUnitId, "adaptive_banner", "true");
        MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.black);

        isMaxBannerInitialized = true;

    }


    public void ShowBanner()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;

        if (!GlobalConstant.IsMaxBannerAd)
            return;


        AdMob_GF.isBannerMax = true;

        MaxSdk.ShowBanner(BannerAdUnitId);


        Debug.Log("ShowMaxBanner");
    }




    public bool IsBannerAdAvailable()
    {
        return _isBannerReady;
    }

    public void HideBanner(bool callback = false)
    {
        MaxSdk.HideBanner(BannerAdUnitId);
        Debug.Log("HideMaxBanner");
        AdMob_GF.isBannerMax = false;





        if (callback)
        {
            AdMob_GF.allowBannerMaxAppOpen = true;
        }
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

        Debug.Log("Max banner is loaded");

    }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        isMaxBannerInitialized = false;
        if (!isMaxBannerInitialized)
        {
            InitializeBannerAds();
            isMaxBannerInitialized = true;
        }


        Debug.Log("Max banner is failed");


    }

    #endregion

    #region MREC Ad Methods

    private void InitializeMRecAds()
    {
        // MRECs are automatically sized to 300x250.
        MaxSdk.CreateMRec(MRecAdUnitId, MaxSdkBase.AdViewPosition.TopLeft);
    }

    private void ToggleMRecVisibility()
    {
        if (!isMRecShowing)
        {
            MaxSdk.ShowMRec(MRecAdUnitId);
            // showMRecButton.GetComponentInChildren<Text>().text = "Hide MREC";
        }
        else
        {
            MaxSdk.HideMRec(MRecAdUnitId);
            // showMRecButton.GetComponentInChildren<Text>().text = "Show MREC";
        }

        isMRecShowing = !isMRecShowing;
    }

    public void ShowMREC()
    {
        MaxSdk.ShowMRec(MRecAdUnitId);
    }

    public void HideMREC()
    {
        MaxSdk.HideMRec(MRecAdUnitId);
    }

    private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
        double revenue = impressionData.Revenue;
        //Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("ad_unit_name", impressionData.AdUnitIdentifier);
        //dic.Add("ad_format", "applovin_Interstitial");

        // AppsFlyerAdRevenue.logAdRevenue(impressionData.NetworkName, AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeApplovinMax, revenue, "USD", dic);


        AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
        // set revenue and currency
        adjustAdRevenue.setRevenue(revenue, "USD");


        adjustAdRevenue.setAdRevenueNetwork(impressionData.NetworkName);
        adjustAdRevenue.setAdRevenueUnit(impressionData.AdUnitIdentifier);

        // track ad revenue
        Adjust.trackAdRevenue(adjustAdRevenue);

        Debug.Log("Max mediation Network: " + impressionData.NetworkName);

        //if (impressionData.NetworkName == "AdMob" || impressionData.NetworkName == "Google Ad Manager Native")
        //{
        //    return;
        //}

        Debug.Log("revenue: " + revenue);
        if (GD.Controller.Instance.firebaseInitialized)
        {


            var impressionParameters = new[] {
  new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
  new Firebase.Analytics.Parameter("ad_source", impressionData.NetworkName),
  new Firebase.Analytics.Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
  new Firebase.Analytics.Parameter("ad_format", impressionData.AdFormat),
  new Firebase.Analytics.Parameter("value", revenue),
  new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
};

            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("paid_ad_impression", impressionParameters);

        }
    }
    #endregion
}