using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablemaxbanner : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;
    [SerializeField] List<GameObject> AvoidPanels;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (otherpanelsAreOff())
        {

            if (AdMob_GF.isBannerMax || AdMob_GF.isBannerAdMob)
            {

                AdMob_GF.CommonHideBanner();


            }


        }
        else
        {

            if (!AdMob_GF.isBannerMax || !AdMob_GF.isBannerAdMob)
            {

                AdMob_GF.CommonBannerShow();


            }



        }



    }

    private bool otherpanelsAreOff()
    {

        if (LoadingAdScreen.isShowing)
            return true;
    
        foreach (var item in AvoidPanels)
        {
            if (item.activeInHierarchy)
                return true;
        }


        foreach (var item in panels)
        {
            if (item.activeInHierarchy)
                return false;
        }
        return true;
    }
}
