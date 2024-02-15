using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{

   
    [HideInInspector]public bool SyringPullCheck = false;
    [HideInInspector] public bool SyringPushCheck = false;

    public void SyringePullComplete()
    {
        SyringPullCheck = true;
    }
    public void SyringePushComplete()
    {
        SyringPushCheck = true;
    }
}
