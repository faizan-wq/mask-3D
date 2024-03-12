using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using RS = UnityEngine.RemoteSettings;
using UnityEngine.Analytics;


public enum BannerPosition
{
    Bottom,
    Top,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Center
};

public enum BannerSize
{
    Banner,
    SmartBanner,
    MediumRectangle,
    IABBanner,
    Leaderboard,
    Adaptive
};

public class AdsIds_GF : MonoBehaviour
{
    public BannerPosition bannerPosition;
    public BannerSize bannerSize;
    public string adAppId;
    public string adMobBanner;
    public string adMobInter;
    public string adMobRewarded;
    public string admobRewardedInter;
    public string Admob_AppOpen_ID;
    public string admobBigBanner;
    public GameObject rewardedInterstitial;

    void Awake()
    {
        AdMob_GF.appId = adAppId;
        AdMob_GF.interstitialID = adMobInter;
        AdMob_GF.rewardedAdID = adMobRewarded;
        AdMob_GF.RewardedInter_id = admobRewardedInter;
        AdMob_GF.AppOpen_ID = Admob_AppOpen_ID;
       // AdMob_GF.rewardedInterstitial = rewardedInterstitial;
        AdMob_GF.bannerPosition = bannerPosition;
        AdMob_GF.bannerSize = bannerSize;
        AdMob_GF.bannerID = adMobBanner;
        AdMob_GF.BigBanner_ID = admobBigBanner;

    }
}