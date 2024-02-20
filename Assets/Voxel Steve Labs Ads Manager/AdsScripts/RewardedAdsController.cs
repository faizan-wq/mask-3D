using UnityEngine;
using UnityEngine.Advertisements;
using ToastPlugin;

public class RewardedAdsController : MonoBehaviour
{
    public static RewardedAdsController Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowRewarded()
    {
        ShowRewardedAd();
    }

    public void ShowRewardedAd()
    {
        LoadingAdScreen.instance.ShowLoadingAdScreen(delegate { AdsManager.Instance.ShowRewardedAd(); });
       
    }
}