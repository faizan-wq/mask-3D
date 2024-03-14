using UnityEngine;
using UnityEngine.Advertisements;
using System;
using GD;
using ToastPlugin;
using UnityEngine.UI;

public class Adscaller_VoxelSteveLabs : MonoBehaviour
{
    [SerializeField] bool IsReward = false;
    [SerializeField] private Text adCounterText;
    float timeCounter = 0;
    float currentTimeScale = 0;

    public void OnEnable()
    {
       // LoadingAdScreen.instance.ShowLoadingAdScreen(delegate { AdsManager.Instance.ShowInterstitial(IsReward); });
        
        this.gameObject.SetActive(false);
    }
}