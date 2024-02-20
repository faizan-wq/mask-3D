using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAdmobbanner : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;
    // Start is called before the first frame update
    void Start()
    {

    }

    //// Update is called once per frame
    //void Update()
    //{


    //    if (otherpanelsAreOff())
    //    {
    //        if (AdMob_GF.isBannerAdMob)
    //        {

    //            AdMob_GF.HideBanner();
    //        }

    //    }
    //    else
    //    {
    //        if (!AdMob_GF.isBannerAdMob)
    //        {

    //            AdMob_GF.ShowBanner();
    //        }
    //    }



    //}

    private bool otherpanelsAreOff()
    {
        foreach (var item in panels)
        {
            if (item.activeInHierarchy)
                return false;
        }
        return true;
    }
}
