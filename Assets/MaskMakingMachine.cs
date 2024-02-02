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
        machineButtonPressComplete = true;
    }



   // public void Enable
}
