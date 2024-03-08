using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablemaxbanner : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;
    [SerializeField] List<GameObject> AvoidPanels;
    [SerializeField] private List<GameObject> PausePanelScreens;
    [SerializeField] private GameObject pausePanel;

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
        //int num=0;
        //for (int i = 0; i < PausePanelScreens.Count; i++)
        //{

        //    if(pausePanel!=null)
        //    {
        //        if(pausePanel.activeInHierarchy)
        //        {
        //            if (PausePanelScreens[i].activeInHierarchy)
        //            {
        //                num = 1;
        //            }

        //        }



        //    }
        //    else
        //    {
        //        break;
        //    }

        //}

        //if (pausePanel != null)
        //{
        //    if (pausePanel.activeInHierarchy)
        //    {
        //        if(num==0)
        //        {
        //            return true;
        //        }
        //    }
        //}




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
