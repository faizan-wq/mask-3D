using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstetialController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    private void Update()
    {
        if (!AdMob_GF.isInterstialAdPresent)
            return;


        if (AdMob_GF.oneMinuteTime < 60)
            AdMob_GF.oneMinuteTime++;

        if (AdMob_GF.oneMinuteTime > GlobalConstant.ActionsAdsTimeLimit)
        {

            AdMob_GF.ShowInterstitial();

            AdMob_GF.oneMinuteTime = 0;


        }



    }
}
