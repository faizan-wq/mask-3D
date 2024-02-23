using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System.Collections.Generic;
//using GoogleMobileAdsMediationTestSuite.Api;
using ToastPlugin;
//using Game.UI;
using com.adjust.sdk;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class AdMob_GF : MonoBehaviour
{
    private static BannerView bannerView, bigBannerView;
    public static BannerView bigBannerViewFreeGold;
    private static BannerView bannerViewAdaptive;
    public static InterstitialAd interstitial;
    private static RewardedInterstitialAd rewardedInterstitialAd;
    public static RewardedAd rewardedAd;
    private static InterstitialAd interstitialHighFloor;
    private static InterstitialAd interstitialMediumfloor;
    private static string outputMessage = "";
    public static bool InitbannerViewOnce = true;
    public static string bannerID,
        Adaptive_bannerID,
        bannerID_gp,
        interstitialID,
        appId,
        rewardedAdID,
        RewardedInter_id,
        InterHighFloorID,
        InterMediumFloorID,
        AppOpen_ID;

    public static BannerSize bannerSize;
    public static BannerSize bannerSize_gp;
    public static BannerSize bannerSizeAdaptive;
    public static BannerPosition bannerPosition;
    public static BannerPosition bannerPosition_gp;
    public static BannerPosition bannerPositionAdaptive;
    private static AdSize adSize;
    private static AdSize adSize_gp;
    private static AdSize adSizeAdaptive;
    private static AdPosition adPosition;
    private static AdPosition adPosition_gp;
    public static RequestFloorType FloorType;
    private static AdPosition adPositionAdaptive;
    public static string debugmsg;



    public static bool isBannerAdMob;
    public static bool isBannerMax;
    public static bool isBigBanner;
    // AppOpen 
    public static bool allowBannerAdMobAppOpen = true;
    public static bool allowBannerMaxAppOpen = true;
    public static bool allowBigBannerAdAppOpen = true;

    private AppOpenAd ad;
    public static bool isShowingAd = false;
    private DateTime loadTime;
    public static bool isInterstialAdPresent;
    public static bool IsShownBanner = false;
    public static AdPosition tempAdPosition = AdPosition.BottomRight;
    public static bool IsShownAdaptive = false;
    public static bool IsRewardedInterstitial = false;
    public static float OneMinuteTime = 0;
    public static float ActionsAdsTime = 0;
    public static float InteractionButtonCount = 0;

    public static GameObject rewardedInterstitial;
    public static AdMob_GF Instance;
    //..................................
    public enum RequestFloorType
    {
        Mediation,
        High,
        Meduim,
        AllPrice
    }

    //public static string appId;
    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    //public static AdMob_GF Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        // MobileAds.SetiOSAppPauseOnBackground(true);
        MobileAds.Initialize(HandleInitCompleteAction);

        // Initialize the Google Mobile Ads SDK.
        // MobileAds.Initialize(appId);


        //Instance = this;
        DontDestroyOnLoad(gameObject);
        switch (bannerPosition)
        {
            case BannerPosition.Bottom:
                adPosition = AdPosition.Bottom;
                break;
            case BannerPosition.Top:
                adPosition = AdPosition.Top;
                break;
            case BannerPosition.TopLeft:
                adPosition = AdPosition.TopLeft;
                break;
            case BannerPosition.TopRight:
                adPosition = AdPosition.TopRight;
                break;
            case BannerPosition.BottomLeft:
                adPosition = AdPosition.BottomLeft;
                break;
            case BannerPosition.BottomRight:
                adPosition = AdPosition.BottomRight;
                break;
            case BannerPosition.Center:
                adPosition = AdPosition.Center;
                break;
        }

        switch (bannerPosition_gp)
        {
            case BannerPosition.Bottom:
                adPosition_gp = AdPosition.Bottom;
                break;
            case BannerPosition.Top:
                adPosition_gp = AdPosition.Top;
                break;
            case BannerPosition.TopLeft:
                adPosition_gp = AdPosition.TopLeft;
                break;
            case BannerPosition.TopRight:
                adPosition_gp = AdPosition.TopRight;
                break;
            case BannerPosition.BottomLeft:
                adPosition_gp = AdPosition.BottomLeft;
                break;
            case BannerPosition.BottomRight:
                adPosition_gp = AdPosition.BottomRight;
                break;
            case BannerPosition.Center:
                adPosition_gp = AdPosition.Center;
                break;
        }

        switch (bannerPositionAdaptive)
        {
            case BannerPosition.Bottom:
                adPositionAdaptive = AdPosition.Bottom;
                break;
            case BannerPosition.Top:
                adPositionAdaptive = AdPosition.Top;
                break;
            case BannerPosition.TopLeft:
                adPositionAdaptive = AdPosition.TopLeft;
                break;
            case BannerPosition.TopRight:
                adPositionAdaptive = AdPosition.TopRight;
                break;
            case BannerPosition.BottomLeft:
                adPositionAdaptive = AdPosition.BottomLeft;
                break;
            case BannerPosition.BottomRight:
                adPositionAdaptive = AdPosition.BottomRight;
                break;
            case BannerPosition.Center:
                adPositionAdaptive = AdPosition.Center;
                break;
        }

        switch (bannerSize)
        {
            case BannerSize.Banner:
                adSize = AdSize.Banner;
                break;
            case BannerSize.SmartBanner:
                adSize = AdSize.SmartBanner;
                break;
            case BannerSize.MediumRectangle:
                adSize = AdSize.MediumRectangle;
                break;
            case BannerSize.IABBanner:
                adSize = AdSize.IABBanner;
                break;
            case BannerSize.Leaderboard:
                adSize = AdSize.Leaderboard;
                break;
            case BannerSize.Adaptive:
                adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
                break;
        }

        switch (bannerSize_gp)
        {
            case BannerSize.Banner:
                adSize_gp = AdSize.Banner;
                break;
            case BannerSize.SmartBanner:
                adSize_gp = AdSize.SmartBanner;
                break;
            case BannerSize.MediumRectangle:
                adSize_gp = AdSize.MediumRectangle;
                break;
            case BannerSize.IABBanner:
                adSize_gp = AdSize.IABBanner;
                break;
            case BannerSize.Leaderboard:
                adSize_gp = AdSize.Leaderboard;
                break;
            case BannerSize.Adaptive:
                adSize_gp = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
                break;
        }

        switch (bannerSizeAdaptive)
        {
            case BannerSize.Banner:
                adSizeAdaptive = AdSize.Banner;
                break;
            case BannerSize.SmartBanner:
                adSizeAdaptive = AdSize.SmartBanner;
                break;
            case BannerSize.MediumRectangle:
                adSizeAdaptive = AdSize.MediumRectangle;
                break;
            case BannerSize.IABBanner:
                adSizeAdaptive = AdSize.IABBanner;
                break;
            case BannerSize.Leaderboard:
                adSizeAdaptive = AdSize.Leaderboard;
                break;
            case BannerSize.Adaptive:
                adSizeAdaptive = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
                break;
        }
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Debug.Log("Initialization complete.");
        debugmsg = "Initialization complete.";
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // the main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (!GD.Controller.stopAds)
                GD.Controller.allowFirebaseAds = true;
            RequestBanner();
            RequestBigBanner();

            RequestInterstitial();
            LoadAdAppOpen();
            Invoke(nameof(AppOpenCheck), 4f);
            CreateAndLoadRewardedAd();
        });
    }

    public void AppOpenCheck()
    {

        Debug.Log("App Open");
        if (checkIfAdOpenready)
        {
            ShowAppOpenAd();


        }

    }




    public static void RequestBanner(AdPosition adPos = AdPosition.BottomRight)
    {

        if (!GD.Controller.allowFirebaseAds)
            return;


        if (!GlobalConstant.IsBannerAd)
        {
            Debug.Log("Banner firebase id is false;");
            return;
        }
#if UNITY_EDITOR
        //string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = bannerID;
#elif UNITY_IOS
		string adUnitId = bannerID;
#else
            string adUnitId = "unexpected_platform";
#endif
        if (adPos != AdPosition.BottomRight)
        {
            tempAdPosition = adPos;
        }
        else
        {
            tempAdPosition = adPosition;
        }

        // Create a 320x50 banner at the top of the screen.
        if (bannerView == null)
        {
            // print(bannerID);
            bannerView = new BannerView(bannerID, adSize, tempAdPosition);
            // Register for ad events.
            bannerView.OnAdLoaded += HandleAdLoaded;
            bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
            bannerView.OnAdLoaded += HandleAdOpened;
            bannerView.OnAdClosed += HandleAdClosed;
            bannerView.OnPaidEvent += BannerView_OnPaidEvent;
            //bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
        }
        var adRequest = new AdRequest.Builder().Build();
        //adRequest.Extras.Add("collapsible", "bottom");
        // Load a banner ad.
        bannerView.LoadAd(adRequest);
        HideBanner();
    }

    public static void RequestBannerAdaptive()
    {
        if (!GlobalConstant.IsAdaptiveBannerAd)
        {
            Debug.Log("Adaptive Banner firebase id is false;");
            return;
        }

        // Create a 320x50 banner at the top of the screen.
        if (bannerViewAdaptive == null)
        {
            //print(bannerID);
            bannerViewAdaptive = new BannerView(Adaptive_bannerID, adSizeAdaptive, adPositionAdaptive); //EditAble
            // Register for ad events.
            bannerViewAdaptive.OnAdLoaded += HandleAdLoaded;
            bannerViewAdaptive.OnAdFailedToLoad += HandleAdFailedToLoad;
            bannerViewAdaptive.OnAdLoaded += HandleAdOpened;
            bannerViewAdaptive.OnAdClosed += HandleAdClosed;
            // bannerView2.OnAdLeavingApplication += HandleAdLeftApplication;
        }

        // Load a banner ad.
        bannerViewAdaptive.LoadAd(CreateAdRequest());
        ShowAdaptiveBanner();
    }


    //    private static void RequestInterstitialMediation()
    //    {
    //        print("Rquest Interstiail");
    //#if UNITY_EDITOR
    //        //string adUnitId = "inter_Mediation_ID";
    //#elif UNITY_ANDROID
    //		string adUnitId = inter_Mediation_ID;
    //#elif UNITY_IOS
    //		string adUnitId = inter_Mediation_ID;
    //#else
    //            string adUnitId = "unexpected_platform";
    //#endif
    //        if (interstitialMediation == null)
    //        {
    //            // Create an interstitial.
    //            // print(interstitialID);
    //            interstitialMediation = new InterstitialAd(inter_Mediation_ID);
    //            // Register for ad events.
    //            interstitialMediation.OnAdLoaded += HandleInterstitialLoaded;
    //            interstitialMediation.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
    //            interstitialMediation.OnAdOpening += HandleInterstitialOpened;
    //            interstitialMediation.OnAdClosed += HandleInterstitialClosed;
    //            //  interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
    //        }

    //        // Load an interstitial ad.


    //        interstitialMediation.LoadAd(CreateAdRequest());
    //    }

    public static void RequestInterstitial()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsInterstitialAd)
            return;
        if (!GlobalConstant.IsInterstitialAdmobAd)
            return;

#if UNITY_EDITOR
        //string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = interstitialID;
#elif UNITY_IOS
		string adUnitId = interstitialID;
#else
            string adUnitId = "unexpected_platform";
#endif

        if (interstitial != null)
        {
            interstitial.Destroy();
        }


        interstitial = new InterstitialAd(interstitialID);
        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnPaidEvent += Interstitial_OnPaid;
        interstitial.LoadAd(CreateAdRequest());
        print("Rquest Interstiail");

    }

    //public static void RequestInterstitialHighFloor()
    //{

    //    if (interstitialHighFloor == null)
    //    {
    //        //  print("interstitial");
    //        //  interstitial.Destroy();

    //        // Create an interstitial.
    //        // print(InterHighFloorID);
    //        interstitialHighFloor = new InterstitialAd(InterHighFloorID);
    //        // Register for ad events.
    //        interstitialHighFloor.OnAdLoaded += HandleInterstitialLoaded;
    //        interstitialHighFloor.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
    //        interstitialHighFloor.OnAdOpening += HandleInterstitialOpened;
    //        interstitialHighFloor.OnAdClosed += HandleInterstitialClosed;

    //    }
    //    AdRequest request = new AdRequest.Builder().Build();
    //    // Load an interstitial ad.
    //    interstitialHighFloor.LoadAd(request);
    //}

    //public static void RequestInterstitialMediumFloor()
    //{

    //    if (interstitialMediumfloor == null)
    //    {
    //        // Create an interstitial.
    //        interstitialMediumfloor = new InterstitialAd(InterMediumFloorID);
    //        // Register for ad events.
    //        interstitialMediumfloor.OnAdLoaded += HandleInterstitialLoaded;
    //        interstitialMediumfloor.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
    //        interstitialMediumfloor.OnAdOpening += HandleInterstitialOpened;
    //        interstitialMediumfloor.OnAdClosed += HandleInterstitialClosed;

    //    }
    //    AdRequest request = new AdRequest.Builder().Build();
    //    // Load an interstitial ad.
    //    interstitialMediumfloor.LoadAd(request);
    //}
    // Returns an ad request with custom ad targeting.
    private static AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }


    public static bool IsInterstitialReady(RequestFloorType type)
    //public static bool IsInterstitialReady()
    {
        switch (type)
        {
            //case RequestFloorType.Mediation:
            //    if (interstitialMediation.IsLoaded())
            //    {
            //        debugmsg = "Mediation Add Loaded ";
            //        return true;
            //    }
            //    else
            //    {
            //        RequestInterstitialMediation();

            //    }
            //    break;

            //case RequestFloorType.High:
            //    if (interstitialHighFloor.IsLoaded())
            //    {
            //        debugmsg = "Mediation High Loaded ";
            //        return true;
            //    }
            //    else
            //    {
            //        RequestInterstitialHighFloor();

            //    }
            //    break;

            //case RequestFloorType.Meduim:
            //    if (interstitialMediumfloor.IsLoaded())
            //    {
            //        debugmsg = "Mediation medium Loaded ";
            //        return true;
            //    }
            //    else
            //    {
            //        RequestInterstitialMediumFloor();

            //    }
            //    break;

            case RequestFloorType.AllPrice:
                if (interstitial != null && interstitial.IsLoaded())
                {
                    debugmsg = "Mediation AllPrice Loaded ";
                    return true;
                }
                else
                {
                    RequestInterstitial();
                }

                break;
        }

        return false;
    }


    public static void ShowInterstitial(bool IsRewarded = false)
    {

        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsInterstitialAd)
            return;
        if (!GlobalConstant.IsInterstitialAdmobAd)
            return;

        IsRewardedInterstitial = IsRewarded;
        AdsManager.isInterstialAdPresent = true;
        if (interstitial != null && interstitial.IsLoaded())
        {
            isInterstialAdPresent = false;
            // FloorType = RequestFloorType.AllPrice;
            debugmsg = " AllPrice Show ";
            interstitial.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }


    private static int count = 0;

    public static void HideBanner(bool callback = false)
    {
        if (bannerView != null)
        {
            bannerView.Hide();
            //Debug.Log("HideBanner");
        }


        isBannerAdMob = false;

        if (callback)
        {
            allowBannerAdMobAppOpen = true;
        }

    }

    public static void HideAdaptiveBanner()
    {
        if (bannerViewAdaptive != null)
            bannerViewAdaptive.Hide();
        //IsRecBanner_show = false;
        //IsShownRect = false;
    }

    public static void ShowBanner()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsBannerAd)
            return;

        IsShownBanner = true;

        if (bannerView != null)
        {
            isBannerAdMob = true;
            bannerView.Show();
            Debug.Log("ShowBanner");
        }

    }
    public static void ShowRectbanner()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsBannerAd)
            return;


        Debug.Log("I'm in Show big banner");
        if (bigBannerView != null)
        {
            isBigBanner = true;
            //bannerunitname = "Rec_banner";
            bigBannerView.Show();

        }
        else
        {
            RequestBigBanner();
        }
    }

    public static void ShowRectbannerFreeGold()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;


        if (!GlobalConstant.IsBannerAd)
            return;


        Debug.Log("I'm in Show big banner");
        if (bigBannerViewFreeGold != null)
        {

            //bannerunitname = "Rec_banner";
            bigBannerViewFreeGold.Show();

        }
        else
        {
            RequestBigBannerFreeGold();
        }
    }


    public static void ShowAdaptiveBanner()
    {
        try
        {
            IsShownAdaptive = true;
            if (bannerViewAdaptive != null)
            {
                bannerViewAdaptive.Show();
                //  IsRecBanner_show = true;
            }
            //else
            //{
            //    RequestBannerAdaptive();
            //}
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Dummy_HideBanner()
    {
        if (bannerView != null)
            bannerView.Hide();
    }

    public static void Dummy_HideAdaptiveBanner()
    {
        if (bannerViewAdaptive != null)
            bannerViewAdaptive.Hide();
    }

    public static void DestroyBanner()
    {
        if (bannerView != null)
            bannerView.Destroy();
    }
    public static void AdmobRewardedShow()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsRewardedAd)
            return;
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            isInterstialAdPresent = true;
            AdsManager.isInterstialAdPresent = true;
        }
        else
        {
            if (interstitial != null && interstitial.IsLoaded())
            {
                if (rewardedInterstitial.activeSelf)
                {
                    rewardedInterstitial.GetComponent<Adscaller_VoxelSteveLabs>().OnEnable();

                }
                else
                {
                    rewardedInterstitial.SetActive(true);
                }
            }
            else
            {
                GD.Controller.Instance.ActionVideo(true);
                IsRewardedInterstitial = false;
                RequestInterstitial();
            }
        }
    }
    public static void ShowRewardedAdmobOrInterstitial()
    {
        // LoadingAdScreen.instance.ShowLoadingAdScreen(delegate { AdsManager.Instance.ShowRewardedAd(); });
        AdsManager.Instance.ShowRewardedAd();

    }

    public static void ShowRewardedAdmob()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;

        if (!GlobalConstant.IsRewardedAd)
        {
            Debug.Log("Rewarded firebase id is false;");
            return;
        }

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            isInterstialAdPresent = true;
            AdsManager.isInterstialAdPresent = true;
        }
        else
        {
            CreateAndLoadRewardedAd();
        }
    }

    public static void CreateAndLoadRewardedAd()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;

        if (!GlobalConstant.IsRewardedAd)
        {
            Debug.Log("Rewarded firebase id is false;");
            return;
        }
#if UNITY_EDITOR
        //string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = rewardedAdID;
#elif UNITY_IPHONE
        string adUnitId = rewardedAdID;
#else
        string adUnitId = "unexpected_platform";
#endif
        // Create new rewarded ad instance.
        // print(rewardedAdID);
        //if (rewardedAd == null)
        {
            rewardedAd = new RewardedAd(rewardedAdID);

            // Called when an ad request has successfully loaded.
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;


            // Called when an ad is shown.
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            rewardedAd.OnPaidEvent += Reward_OnPaid;
        }
        // Create an empty ad request.
        AdRequest request = CreateAdRequest();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }
    //    public static void CreateAndLoadRewardedAd_Mediation()
    //    {
    //#if UNITY_EDITOR
    //        //string adUnitId = "unused";
    //#elif UNITY_ANDROID
    //        string adUnitId = rewardedAdID;
    //#elif UNITY_IPHONE
    //        string adUnitId = rewardedAdID;
    //#else
    //        string adUnitId = "unexpected_platform";
    //#endif
    //        // Create new rewarded ad instance.
    //        // print(rewardedAdID);
    //        rewarded_mediation = new RewardedAd(Rewwarded_mediationID);

    //        // Called when an ad request has successfully loaded.
    //        rewarded_mediation.OnAdLoaded += HandleRewardedAdLoaded;


    //        // Called when an ad is shown.
    //        rewarded_mediation.OnAdOpening += HandleRewardedAdOpening;
    //        // Called when an ad request failed to show.
    //        rewarded_mediation.OnAdFailedToShow += HandleRewardedAdFailedToShow;
    //        // Called when the user should be rewarded for interacting with the ad.
    //        rewarded_mediation.OnUserEarnedReward += HandleUserEarnedReward;
    //        // Called when the ad is closed.
    //        rewarded_mediation.OnAdClosed += HandleRewardedAdClosed;

    //        // Create an empty ad request.
    //        AdRequest request = CreateAdRequest();
    //        // Load the rewarded ad with the request.
    //        rewarded_mediation.LoadAd(request);
    //    }

    #region Banner callback handlers

    static void HandleAdLoaded(object sender, EventArgs args)
    {
        //   print("HandleAdLoaded event received.");
    }

    static void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //  print("HandleFailedToReceiveAd event received with message");
    }

    static void HandleAdOpened(object sender, EventArgs args)
    {
        //  print("HandleAdOpened event received");
    }

    static void HandleAdClosing(object sender, EventArgs args)
    {
        //  print("HandleAdClosing event received");
    }

    static void HandleAdClosed(object sender, EventArgs args)
    {
        //  print("HandleAdClosed event received");
    }

    static void HandleAdLeftApplication(object sender, EventArgs args)
    {
        //  print("HandleAdLeftApplication event received");
    }

    #endregion


    #region Interstitial callback handlers

    static void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    static void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        debugmsg = "HandleInterstitialFailedToLoad event received with message";
        print("HandleInterstitialFailedToLoad event received with message");
    }

    static void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    static void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    static void HandleInterstitialClosed(object sender, EventArgs args)
    {
        if (IsRewardedInterstitial)
        {
            GD.Controller.Instance.ActionVideo(true);
            IsRewardedInterstitial = false;
        }

        RequestInterstitial();
    }

    static void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        // print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardedAd callback handlers

    public static void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public static void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
        "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public static void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public static void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
        "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public static void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //if (!rewarded_mediation.IsLoaded())
        //{
        //    CreateAndLoadRewardedAd_Mediation();
        //}
        if (!rewardedAd.IsLoaded())
        {
            CreateAndLoadRewardedAd();
        }

        //MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public static void HandleUserEarnedReward(object sender, Reward args)
    {
        print("Rewarded Claimed");
        GD.Controller.Instance.ActionVideo(true);
        ToastHelper.ShowToast("Reward hase added", false);
        // Data_GF.RewardedAdWatched();
        //Data_GF.RewardedAdWatched();
    }

    #endregion

    //AppOpen 

    private bool IsAdAvailable
    {
        get
        {
            return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;

        }
    }
    public static bool checkIfAdOpenready = false;
    public void LoadAdAppOpen()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.isAppOpen)
        {
            Debug.Log("Rewarded firebase id is false;");
            return;
        }
        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(AppOpen_ID, ScreenOrientation.LandscapeLeft, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                checkIfAdOpenready = false;
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());

                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
            loadTime = DateTime.UtcNow;
            Debug.Log("End Request  ");
            checkIfAdOpenready = true;
        }));
    }

    public void ShowAppOpenAd()
    {
        if (!GD.Controller.allowFirebaseAds)
            return;

        if (!GlobalConstant.isAppOpen)
        {
            Debug.Log("App Open firebase id is false;");
            return;
        }

        if (isInterstialAdPresent || AdsManager.isInterstialAdPresent)
            return;



        if (isShowingAd)
        {
            Debug.Log("ISShowingAd");
            return;
        }




        if (ad == null)
        {
            Debug.Log("ad == null");
            LoadAdAppOpen();
            return;
        }


        Debug.Log("App Open is Showing");
        // Register for ad events.
        this.ad.OnAdDidDismissFullScreenContent += (sender, args) =>
        {

            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                Debug.Log("AppOpenAd dismissed.");
                isShowingAd = false;

                if (allowBigBannerAdAppOpen)
                {

                    ShowRectbanner();

                }
                allowBannerAdMobAppOpen = false;
                allowBannerMaxAppOpen = false;
                allowBigBannerAdAppOpen = false;

                //if(!InitbannerViewOnce)
                // {
                //     ShowBanner();
                //     ShowRectbanner();

                //     InitbannerViewOnce = true;

                // }

                //AdsManager.Instance.ShowBanner();
                AdMob_GF.ShowBanner();


                AppOpenToggle();
                if (this.ad != null)
                {
                    this.ad.Destroy();
                    this.ad = null;
                }
                LoadAdAppOpen();

            });
        };
        this.ad.OnAdFailedToPresentFullScreenContent += (sender, args) =>
        {
            isShowingAd = false;
            var msg = args.AdError.GetMessage();
            allowBannerAdMobAppOpen = false;
            allowBannerMaxAppOpen = false;
            allowBigBannerAdAppOpen = false;
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                // statusText.text = "AppOpenAd present failed, error: " + msg;
                if (this.ad != null)
                {
                    this.ad.Destroy();
                    this.ad = null;
                }
            });

        };
        this.ad.OnAdDidPresentFullScreenContent += (sender, args) =>
        {
            isShowingAd = true;
            allowBannerAdMobAppOpen = false;
            allowBannerMaxAppOpen = false;
            allowBigBannerAdAppOpen = false;
            //if(isBannerAdMob)
            //{
            //    HideBanner(true);
            //}
            if (isBannerAdMob)
            {
                AdMob_GF.HideBanner(true);
            }
            if (isBannerMax)
            {
                //AdsManager.Instance.HideBanner(true);

            }
            if (isBigBanner)
            {
                HideBigBanner(true);
            }
            BlackBarBeforebanner.HideBlackBar();
            MobileAdsEventExecutor.ExecuteInUpdate(() => { Debug.Log("AppOpenAd presented."); });
        };
        this.ad.OnAdDidRecordImpression += (sender, args) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                Debug.Log("AppOpenAd recorded an impression.");
                LoadAdAppOpen();
            });
        };
        this.ad.OnPaidEvent += (sender, args) =>
        {
            string currencyCode = args.AdValue.CurrencyCode;
            long adValue = args.AdValue.Value;
            string suffix = "AppOpenAd received a paid event.";
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}", suffix, currencyCode, adValue);
                //statusText.text = msg;
            });
            Debug.Log("revenue: " + adValue);
            Send_Revenu(adValue, "App Open");
        };
        if (toggle == false)
            return;


        ad.Show();
        toggle = false;
        AppOpenToggle();

    }

    private bool toggle = true;

    void AppOpenToggle()
    {
        toggle = true;

    }



    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            if (isInterstialAdPresent || AdsManager.isInterstialAdPresent)
            {
                isInterstialAdPresent = false;
                AdsManager.isInterstialAdPresent = false;
                return;
            }
            ShowAppOpenAd();


            // ShowAdIfReady();
        }



    }
    //ONPAID EVENTS............

    static void BannerView_OnPaidEvent(object sender, AdValueEventArgs impressionData)
    {
        Send_Revenu(impressionData.AdValue.Value, "Banner");
    }

    static void BigBanner_OnPaidEvent(object sender, AdValueEventArgs impressionData)
    {
        Send_Revenu(impressionData.AdValue.Value, "Big Banner");
    }
    static void Interstitial_OnPaid(object sender, AdValueEventArgs impressionData)
    {
        Send_Revenu(impressionData.AdValue.Value, "Interstitail");
    }


    static void Reward_OnPaid(object sender, AdValueEventArgs impressionData)
    {
        Send_Revenu(impressionData.AdValue.Value, "Rewarded");
    }
    public static void Send_Revenu(double rev, string unit_name)
    {
        double revenue = rev / 1000000;
        if (GD.Controller.Instance.firebaseInitialized)
        {
            var impressionParameters = new[] {
      new Firebase.Analytics.Parameter("ad_platform", "admob"),
      new Firebase.Analytics.Parameter("ad_source", "admob"),
      new Firebase.Analytics.Parameter("ad_unit_name",unit_name),
      new Firebase.Analytics.Parameter("ad_format", unit_name),
      new Firebase.Analytics.Parameter("value", revenue),
      new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
    };

            Firebase.Analytics.FirebaseAnalytics.LogEvent("aa_ads_Revenue", impressionParameters);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("paid_ad_impression", impressionParameters);
        }
        AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
        // set revenue and currency
        adjustAdRevenue.setRevenue(revenue, "USD");

        Debug.Log("revenue: " + revenue);
        // track ad revenue
        Adjust.trackAdRevenue(adjustAdRevenue);
    }
    static void BigBannerFastCash_OnPaidEvent(object sender, AdValueEventArgs impressionData)
    {
        Send_Revenu(impressionData.AdValue.Value, "Big Banner Fast Cash");
    }

    #region Big Banner

    public static void RequestBigBanner()
    {

        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsBannerAd)
            return;


        Debug.Log("I'm in Request big banner");
        if (bigBannerView != null)
        {
            bigBannerView.Destroy();
        }

        string BigBannerIds = "";
        BigBannerIds = "ca-app-pub-1042488596199134/5145422375";
#if UNITY_IOS
        BigBannerIds = "ca-app-pub-9339267581233229/4752115296";
#endif
        //if (TestIds)
        //{
        //BigBannerIds = "ca-app-pub-3940256099942544/6300978111";
        //}

        // Register for ad events.
        //    adSize1 = ;
        bigBannerView = new BannerView(BigBannerIds, AdSize.MediumRectangle, AdPosition.TopLeft);
        bigBannerView.OnAdLoaded += HandleBigBannerAdLoaded;
        bigBannerView.OnAdFailedToLoad += HandleBigBannerAdFailedToLoad;
        bigBannerView.OnAdLoaded += HandleBigBannerAdOpened;
        bigBannerView.OnAdClosed += HandleBigBannerAdClosed;
        bigBannerView.OnPaidEvent += BigBanner_OnPaidEvent;
        // Load a banner ad.
        bigBannerView.LoadAd(CreateAdRequestBigBanner());
        //   IsRectBanner = true;
        bigBannerView.Hide();


    }

    public static void HideBigBanner(bool isCallfromAppOpen = false)
    {
        if (bigBannerView != null)
            bigBannerView.Hide();

        isBigBanner = false;

        if (isCallfromAppOpen)
        {
            allowBigBannerAdAppOpen = true;
        }

    }



    private static AdRequest CreateAdRequestBigBanner()
    {
        return new AdRequest.Builder().Build();
    }

    //private static void OnBigBannerAdPaidEvent(object sender, AdValueEventArgs e)
    //{

    //    Debug.Log("Big Banner paid");
    //    PaidEventCaller.Revenue_ReportAdmob(e.AdValue, " big banner ad");

    //}


    static void HandleBigBannerAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    static void HandleBigBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {


        print("HandleFailedToReceiveAd event received with message: " + args.LoadAdError);
    }


    static void HandleBigBannerAdOpened(object sender, EventArgs args)
    {

        print("HandleAdOpened event received");
    }



    static void HandleBigBannerAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");

    }

    static void HandleBigBannerAdLeftApplication(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }




    #endregion



    #region Big Banner Free Gold
    public const bool TestIds = true;
    public static void RequestBigBannerFreeGold()
    {

        if (!GD.Controller.allowFirebaseAds)
            return;
        if (!GlobalConstant.IsBigBannerFreeGoldAd)
            return;


        Debug.Log("I'm in Request big banner");
        if (bigBannerViewFreeGold != null)
        {
            bigBannerViewFreeGold.Destroy();
        }

        string BigBannerIds = "";
        BigBannerIds = "ca-app-pub-1042488596199134~2549154013";
#if UNITY_IOS
        BigBannerIds = "ca-app-pub-9339267581233229/4752115296";
#endif
        if (TestIds)
        {
            BigBannerIds = "ca-app-pub-3940256099942544/6300978111";
        }

        // Register for ad events.
        //    adSize1 = ;
        bigBannerViewFreeGold = new BannerView(BigBannerIds, AdSize.MediumRectangle, AdPosition.TopLeft);
        bigBannerViewFreeGold.OnAdLoaded += HandleBigBannerAdLoadedFreeGold;
        bigBannerViewFreeGold.OnAdFailedToLoad += HandleBigBannerAdFailedToLoadFreeGold;
        bigBannerViewFreeGold.OnAdLoaded += HandleBigBannerAdOpenedFreeGold;
        bigBannerViewFreeGold.OnAdClosed += HandleBigBannerAdClosedFreeGold;
        bigBannerViewFreeGold.OnPaidEvent += BigBannerFastCash_OnPaidEvent;
        // Load a banner ad.
        bigBannerViewFreeGold.LoadAd(CreateAdRequestBigBannerFreeGold());
        //   IsRectBanner = true;



    }


    private static AdRequest CreateAdRequestBigBannerFreeGold()
    {
        return new AdRequest.Builder().Build();
    }

    //private static void OnBigBannerAdPaidEvent(object sender, AdValueEventArgs e)
    //{

    //    Debug.Log("Big Banner paid");
    //    PaidEventCaller.Revenue_ReportAdmob(e.AdValue, " big banner ad");

    //}


    static void HandleBigBannerAdLoadedFreeGold(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    static void HandleBigBannerAdFailedToLoadFreeGold(object sender, AdFailedToLoadEventArgs args)
    {


        print("HandleFailedToReceiveAd event received with message: " + args.LoadAdError);
    }


    static void HandleBigBannerAdOpenedFreeGold(object sender, EventArgs args)
    {

        print("HandleAdOpened event received");
    }



    static void HandleBigBannerAdClosedFreeGold(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");

    }

    static void HandleBigBannerAdLeftApplicationFreeGold(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }

    public static void HideBigBannerFreeGold(bool isCallfromAppOpen = false)
    {
        if (bigBannerViewFreeGold != null)
            bigBannerViewFreeGold.Hide();





    }


    #endregion


}