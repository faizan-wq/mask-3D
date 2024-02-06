using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMakingMachine : MonoBehaviour
{


    [HideInInspector] public bool machineShakingComplete;
    [HideInInspector] public bool machineButtonPressComplete;
   
    public void EnableMachineShaking()
    {
        machineShakingComplete = true;


    }

    public void EnableButtonPressed()
    {
        MaskMakingLevel.Instance.maskMakingController.Tutorial1.SetActive(true);
        machineButtonPressComplete = true;
    }

   


   // public void Enable
}
