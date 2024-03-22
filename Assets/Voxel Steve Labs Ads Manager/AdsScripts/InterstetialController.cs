using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstetialController : MonoBehaviour
{
    public float number;
    public static  bool interstetialAdShowing;
    private float startingTime;
    private WaitForSecondsRealtime realtimeWait = new WaitForSecondsRealtime(1);

    // Start is called before the first frame update
    void Start()
    {
        //startingTime=Time.realtimeSinceStartup;
        startingTime = 0;
        DontDestroyOnLoad(this);
    }

    

  
    float secondsPassed=0;
    
    private void WithoutCoroutineTimer()
    {

        
        secondsPassed += Time.deltaTime;


        AdMob_GF.oneMinuteTime = Mathf.Clamp(GlobalConstant.ActionsAdsTimeLimit - (int)secondsPassed, 0, 1000f);
        if (AdMob_GF.oneMinuteTime ==0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LoadingAdScreen.instance.ShowLoadingAdScreen(() => { AdsManager.Instance.ShowInterstitial(false); });
                secondsPassed = 0;
                AdMob_GF.oneMinuteTime = GlobalConstant.ActionsAdsTimeLimit;
                return;
            }
        }


       
    }
    //private void Update()
    //{

    //    if (AdMob_GF.isShowingAd)
    //        return;
    //    //if (interstetialAdShowing)
    //    //    return;

    //    WithoutCoroutineTimer();




    //}
}
