using UnityEngine;
using UnityEngine.Advertisements;
public class Test_AdsCaller : MonoBehaviour
{
    public static Test_AdsCaller Insatance;
    public Native_AdsController native_AdsController;
    public void NativeAdsCall()
    {
        native_AdsController.RequestNativeAd();
    }
    public void BannerRequest()
    {
      //  AdMob_GF.RequestBanner();
    }
    public void AdmobInterstial()
    {
        // if (AdMob_VoxelSteveLabs.Instance.IsInterstitialReady())
        // {
        //     AdMob_VoxelSteveLabs.Instance.ShowInterstitial();
        // }
    }
    public void AdmobRewardedVideo()
    {
        LoadingAdScreen.instance.ShowLoadingAdScreen(delegate { AdsManager.Instance.ShowRewardedAd(); });
       
    }
    public void ShowUnity()
    {
        //if (AdMob_VoxelSteveLabs.IsUnityAds && Advertisement.IsReady())
        //{
        //    Advertisement.Show();
        //}
    }
    public void ShowUnityRewardedAd()
    {
        //if (AdMob_VoxelSteveLabs.IsUnityAds && Advertisement.IsReady("rewardedVideo"))
        //{
        //    var options = new ShowOptions { resultCallback = HandleShowResult };
        //    Advertisement.Show("rewardedVideo", options);
        //}
    }
    //private void HandleShowResult(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            Debug.Log("The ad was successfully shown.");
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("The ad was skipped before reaching the end.");
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }
    //}
}
