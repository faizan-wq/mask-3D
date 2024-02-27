using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstetialController : MonoBehaviour
{
    public float number;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    private void Update()
    {
       
        if (AdsManager.isInterstialAdPresent)
            return;




        
        if (AdMob_GF.oneMinuteTime < 60)
            AdMob_GF.oneMinuteTime+=Time.deltaTime;

        number = AdMob_GF.oneMinuteTime;

        if (AdMob_GF.oneMinuteTime > GlobalConstant.ActionsAdsTimeLimit)
        {

            //AdMob_GF.ShowInterstitial();
            LoadingAdScreen.instance.ShowLoadingAdScreen(()=> { AdsManager.Instance.ShowInterstitial(false); });
            


            AdMob_GF.oneMinuteTime = 0;


        }



    }
}
