using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMakingController : MonoBehaviour
{

    [SerializeField] private Animator maskMakingMachine;
    [SerializeField] private Animator mask;
   
    private bool shakingOfMachineStarted;
    

    private bool pressingOfMachineStarted;
    


    private bool maskOfMachineStarted;
    





    public void MachineShaking()
    {

        Debug.Log("PerformAnimationMaskmaking");
        if (!shakingOfMachineStarted)
        {
            PerformAnimationMaskmaking("Shake");
           
            shakingOfMachineStarted = true;
        }
        if(!pressingOfMachineStarted && maskMakingMachine.GetComponent<MaskMakingMachine>().machineShakingComplete && Input.GetMouseButton(0))
        {
            PerformAnimationMaskmaking("Pressed");
            pressingOfMachineStarted = true;
        }
        if(maskMakingMachine.GetComponent<MaskMakingMachine>().machineButtonPressComplete)
        {
            maskMakingMachine.GetComponent<MaskMakingMachine>().machineButtonPressComplete = false;
            mask.Play("Liquide");
        }

    }















    private void PerformAnimationMaskmaking(string name)
    {
        maskMakingMachine.SetBool(name,true);
    }
    private void PerformAnimationMask(string name)
    {
        mask.SetBool(name,true);
    }







}
[Serializable]
public class MaskMakingTasks
{
    public bool Started = false;
    public bool Completed=false;
}