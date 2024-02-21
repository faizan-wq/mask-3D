using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBigbanner : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;

  


    // Update is called once per frame
    void Update()
    {


        if (otherpanelsAreOff())
        {
            if (AdMob_GF.isBigBanner)
            {

                AdMob_GF.HideBigBanner();
            }

        }
        else
        {
            if (!AdMob_GF.isBigBanner)
            {

                AdMob_GF.ShowRectbanner();
            }
        }



    }

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
