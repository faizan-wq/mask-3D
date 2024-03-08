using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRectBanner : MonoBehaviour
{

   
    [SerializeField] private bool Specialcase = false;


    private void OnEnable()
    {
        if (!AdMob_GF.isBigBanner)
        {
            AdMob_GF.ShowRectbanner();
        }

    }
    private void Update()
    {
        if(Specialcase)
        {
            if(!AdMob_GF.isBigBanner)
            {
                AdMob_GF.ShowRectbanner();
            }
        }
    }
    private void OnDisable()
    {
        AdMob_GF.HideBigBanner();
       
    }
   
}
