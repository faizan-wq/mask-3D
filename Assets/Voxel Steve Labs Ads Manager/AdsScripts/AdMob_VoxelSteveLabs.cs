//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;
//using ToastPlugin;


//public class AdMob_VoxelSteveLabs : MonoBehaviour
//{
//    public static AdMob_VoxelSteveLabs Instance;
//    private void Awake()
//    {
//        Instance = this;
//        MobileAds.SetiOSAppPauseOnBackground(true);
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(HandleInitCompleteAction);
//        DontDestroyOnLoad(gameObject);
//    }

//    private BannerView bannerView;
//    private BannerView rectBannerView;
//    private InterstitialAd interstitial;
//    public RewardedAd rewardedAd;

//    public static bool IsShownRect=false;
//    public static bool IsShownBanner = false;

//    private string outputMessage = "";
//    [HideInInspector]
//    public string bannerID,rectBannerID, interstitialID, rewardedAdID;
//    [HideInInspector]
//    public BannerSize bannerSize;
//    [HideInInspector]
//    public BannerPosition bannerPosition;
//    private AdSize adSize;
//    private AdPosition adPosition;
//    public string OutputMessage
//    {
//        set { outputMessage = value; }
//    }

//    System.IDisposable LevelIDisposable, GameOverIDisposable;
//    void Start()
//    {

//        switch (bannerPosition)
//        {
//            case BannerPosition.Bottom:
//                adPosition = AdPosition.Bottom;
//                break;
//            case BannerPosition.Top:
//                adPosition = AdPosition.Top;
//                break;
//            case BannerPosition.TopLeft:
//                adPosition = AdPosition.TopLeft;
//                break;
//            case BannerPosition.TopRight:
//                adPosition = AdPosition.TopRight;
//                break;
//            case BannerPosition.BottomLeft:
//                adPosition = AdPosition.BottomLeft;
//                break;
//            case BannerPosition.BottomRight:
//                adPosition = AdPosition.BottomRight;
//                break;
//            case BannerPosition.Center:
//                adPosition = AdPosition.Center;
//                break;
//        }

//        switch (bannerSize)
//        {
//            case BannerSize.Banner:
//                adSize = AdSize.Banner;
//                break;
//            case BannerSize.SmartBanner:
//                adSize = AdSize.SmartBanner;
//                break;
//            case BannerSize.MediumRectangle:
//                adSize = AdSize.MediumRectangle;
//                break;
//            case BannerSize.IABBanner:
//                adSize = AdSize.IABBanner;
//                break;
//            case BannerSize.Leaderboard:
//                adSize = AdSize.Leaderboard;
//                break;
//            case BannerSize.Adaptive:

//                float widthInPixels = Screen.safeArea.width > 0 ? Screen.safeArea.width : Screen.width;
//                int width = (int)(widthInPixels / MobileAds.Utils.GetDeviceScale());
//                MonoBehaviour.print("requesting width: " + width.ToString());
//                adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(width);

//                break;
//        }

     
       
//    }
//    private void HandleInitCompleteAction(InitializationStatus initstatus)
//    {
//        Debug.Log("Initialization complete.");

//        // Callbacks from GoogleMobileAds are not guaranteed to be called on
//        // the main thread.
//        // In this example we use MobileAdsEventExecutor to schedule these calls on
//        // the next Update() loop.
//        MobileAdsEventExecutor.ExecuteInUpdate(() =>
//        {
//            RequestInterstitial();
//            CreateAndLoadRewardedAd();
      
//        });
//    }


//    public void RequestBanner()
//    {
//        IsShownBanner = true;

//#if UNITY_EDITOR
//         string adUnitId = "unused";
//#elif UNITY_ANDROID
//		                string adUnitId = bannerID;
//#elif UNITY_IOS
//		                string adUnitId = bannerID;
//#else
//                            string adUnitId = "unexpected_platform";
//#endif

//         // Create a 320x50 banner at the top of the screen.
//         if (bannerView != null)
//         {
//             bannerView.Destroy();
//         }
//         bannerView = new BannerView(adUnitId, adSize, adPosition);
//         // Register for ad events.
//         bannerView.OnAdLoaded += HandleAdLoaded;
//         bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
//         bannerView.OnAdLoaded += HandleAdOpened;
//         bannerView.OnAdClosed += HandleAdClosed;
//         //bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
//         // Load a banner ad.
//         bannerView.LoadAd(CreateAdRequest());
  






//    }
    
    
//    public void RequestRectBanner()
//    {


//        IsShownRect = true;
//#if UNITY_EDITOR
//                string adUnitId = "unused";
//#elif UNITY_ANDROID
//		                string adUnitId = rectBannerID;
//#elif UNITY_IOS
//		                string adUnitId = rectBannerID;
//#else
//                            string adUnitId = "unexpected_platform";
//#endif

//                // Create a 320x50 banner at the top of the screen.
//                if (rectBannerView != null)
//                {
//                    rectBannerView.Destroy();
//                }
//                rectBannerView = new BannerView(adUnitId,AdSize.MediumRectangle,AdPosition.TopLeft);
//                // Register for ad events.
//                rectBannerView.OnAdLoaded += HandleAdLoaded;
//                rectBannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
//                rectBannerView.OnAdLoaded += HandleAdOpened;
//                rectBannerView.OnAdClosed += HandleAdClosed;
//               // rectBannerView.OnAdLeavingApplication += HandleAdLeftApplication;
//                // Load a banner ad.
//                rectBannerView.LoadAd(CreateAdRequest());







//    }

//    #region  Interstitial

//    [SerializeField] private List<InterstitialFlooring> interstitialFlooring;
//    [System.Serializable]
//    public class InterstitialFlooring
//    {
//        public string ID;
//        private InterstitialAd interstitial;

//        public void Show()
//        {
//            if (interstitial != null)
//                interstitial.Show();
//            //Request();
//        }

//        public bool IsReady
//        {
//            get
//            {
//                if (interstitial!=null && interstitial.IsLoaded())
//                {
//                    return true;
//                }
//                else
//                {
//                    Request();
//                    return false;
//                }
//            }
//        }
        
        
//        public void Request()
//        {
   

//            if (interstitial != null)
//            {
//                interstitial.Destroy();
//            }
//            // Create an interstitial.
//            interstitial = new InterstitialAd(ID.Trim());
//            // Register for ad events.
//            interstitial.OnAdLoaded += delegate(object sender, EventArgs args)
//            {
                
//            };
//            interstitial.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
//            {
                
//            };
//            interstitial.OnAdOpening += delegate(object sender, EventArgs args)
//            {
                
//            };
          
//            interstitial.OnAdClosed += delegate(object sender, EventArgs args)
//            {
//                Request();
//            };
//            interstitial.LoadAd(CreateAdRequest());
//        }
        
//        private AdRequest CreateAdRequest()
//        {
//            return new AdRequest.Builder().Build();
//        }
        
        
        
//    }

//    public void ShowInterstitial()
//    {
//        for (int i = 0; i < interstitialFlooring.Count; i++)
//        {
//            if (interstitialFlooring[i].IsReady)
//            {
//                interstitialFlooring[i].Show();
//                return;
//            }
//        }
//    }

//    void RequestInterstitial()
//    {
//        for (int i = 0; i < interstitialFlooring.Count; i++)
//        {
//            interstitialFlooring[i].Request();
//        }
//    }
    
//    #endregion
    
    
    

    
//    private AdRequest CreateAdRequest()
//    {
//        return new AdRequest.Builder().Build();
//    }


//    public void HideBanner()
//    {
//        if (bannerView != null)
//            bannerView.Hide();
//        IsShownBanner = false;
//    }
//    public void HideBannerDummy()
//    {
//        if (bannerView != null)
//            bannerView.Hide();
//    }
//    public void DestroyBanner()
//    {
//        if (bannerView != null)
//            bannerView.Destroy();
//    }

//    public void HideRectBanner()
//    {
//        if (rectBannerView != null)
//            rectBannerView.Destroy();
//        IsShownRect = false;

//    }
//    public void HideRectBannerDummy()
//    {
//        if (rectBannerView != null)
//            rectBannerView.Destroy();
//    }
    
//    public void ShowRewardedAdmob()
//    {
//        if (rewardedAd.IsLoaded())
//        {
//            rewardedAd.Show();

//        }
//        else
//        {
//            CreateAndLoadRewardedAd();
//            MonoBehaviour.print("Rewarded ad is not ready yet");
//        }
//    }
//    public void CreateAndLoadRewardedAd()
//    {

    
//#if UNITY_EDITOR
//         string adUnitId = "unused";
//#elif UNITY_ANDROID
//                string adUnitId = rewardedAdID;
//#elif UNITY_IPHONE
//                string adUnitId = rewardedAdID;
//#else
//                string adUnitId = "unexpected_platform";
//#endif
//         // Create new rewarded ad instance.

//         rewardedAd = new RewardedAd(adUnitId);

//         // Called when an ad request has successfully loaded.
//         rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//         // Called when an ad request failed to load.
//        // rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//         // Called when an ad is shown.
//         rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//         // Called when an ad request failed to show.
//         rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//         // Called when the user should be rewarded for interacting with the ad.
//         rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//         // Called when the ad is closed.
//         rewardedAd.OnAdClosed += HandleRewardedAdClosed;

//         // Create an empty ad request.
//         AdRequest request = CreateAdRequest();
//         // Load the rewarded ad with the request.
//         rewardedAd.LoadAd(request);
   
//    }




//    #region Banner callback handlers

//    public void HandleAdLoaded(object sender, EventArgs args)
//    {
//        print("HandleAdLoaded event received.");
//    }

//    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {


       
//    }

//    public void HandleAdOpened(object sender, EventArgs args)
//    {
//        print("HandleAdOpened event received");
//    }

//    public void HandleAdClosing(object sender, EventArgs args)
//    {
//        print("HandleAdClosing event received");
//    }

//    public void HandleAdClosed(object sender, EventArgs args)
//    {
//        print("HandleAdClosed event received");
//    }

//    public void HandleAdLeftApplication(object sender, EventArgs args)
//    {
//        print("HandleAdLeftApplication event received");
//    }

//    #endregion


//    #region RewardedAd callback handlers

//    public void HandleRewardedAdLoaded(object sender, EventArgs args)
//    {
//        // MonoBehaviour.print("HandleRewardedAdLoaded event received");
//    }

//    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
//    {
//        //MonoBehaviour.print(
//        //"HandleRewardedAdFailedToLoad event received with message: " + args.Message);
//    }

//    public void HandleRewardedAdOpening(object sender, EventArgs args)
//    {
//        //MonoBehaviour.print("HandleRewardedAdOpening event received");
//    }

//    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        //MonoBehaviour.print(
//        //"HandleRewardedAdFailedToShow event received with message: " + args.Message);
//    }

//    public void HandleRewardedAdClosed(object sender, EventArgs args)
//    {
//        CreateAndLoadRewardedAd();
//        //MonoBehaviour.print("HandleRewardedAdClosed event received");
//    }

//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        GD.Controller.Instance.ActionVideo(true);
//        ToastHelper.ShowToast("Reward hase added", false);
//    }

//    #endregion

//}
