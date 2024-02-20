//using System;

//using UnityEngine;
//using UnityEngine.Advertisements;
//public enum BannerPosition { Bottom, Top, TopLeft, TopRight, BottomLeft, BottomRight, Center };
//public enum BannerSize { Banner, SmartBanner, MediumRectangle, IABBanner, Leaderboard, Adaptive };
//public class AdsIds_VoxelSteveLabs : MonoBehaviour
//{
//    public static bool startAd = false;
//    public static int adsCount = 0;
//    public BannerPosition bannerPosition;
//    public BannerSize bannerSize;
   
//    public string adMobBanner;
//    public string adMobInterstitial;
//    public string adMobRewarded;
//    public string rectBanner;
//    public string unityAdsId;

//    public static int AdPriority0 = 0;
//    public static int AdPriority1 = 1;
//    public static int AdPriority2 = 2;
//    public static int AdPriority3 = 3;
//    public static int AdPriority4 = 4;
//    public AdMob_VoxelSteveLabs admob;

//    void Awake()
//    {

//        if (Application.internetReachability == NetworkReachability.NotReachable)
//        {
//            AdPriority0 = 0;
//            AdPriority1 = 1;
//            AdPriority2 = 2;
//            AdPriority3 = 3;
//            AdPriority4 = 4;
//        }

//        admob.bannerPosition = bannerPosition;
//        admob.bannerSize = bannerSize;
//        admob.rewardedAdID = adMobRewarded;
//        //Native_AdsController.nativeAdsID = adMobNative;
//        admob.bannerID = adMobBanner.Trim();
//        admob.interstitialID = adMobInterstitial.Trim();

//        admob.rectBannerID = rectBanner.Trim();
       
//    }

//}
