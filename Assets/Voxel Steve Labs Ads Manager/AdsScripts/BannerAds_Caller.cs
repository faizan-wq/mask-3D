using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAds_Caller : MonoBehaviour
{
    [SerializeField] private bool IsBanner = false;
    [SerializeField] private bool IsTopBanner = false;
    [SerializeField] private bool IsAdaptiveBanner = false;


    private void OnEnable()
    {
        if (GD.Controller.Instance.IsRemoveAds)
        {
            return;
        }

        //AdsManager.Instance.HideBanner();
        BannerCalleGPr();
    }
   




    public void BannerCalleGPr()
    {

      //  AdsManager.Instance.ShowBanner();
    }

    private void OnDisable()
    {
      //  AdsManager.Instance.HideBanner();
    }
}