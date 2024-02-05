using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMakingController : MonoBehaviour
{

    [SerializeField] private Animator maskMakingMachine;
    [SerializeField] private Animator mask;
    [SerializeField] private Animator takingMaskOff;
    private bool masktakingStarted;
   
    private bool shakingOfMachineStarted;
    

    private bool pressingOfMachineStarted;
    


  





    public void MachineShaking()
    {

        Debug.Log("PerformAnimationMaskmaking");
        if (!shakingOfMachineStarted)
        {
            ChangeColorOfMask();
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


    private void ChangeColorOfMask()
    {
        foreach (var item in mask.GetComponent<MeshRenderer>().materials)
        {
            item.color = MaskMakingLevel.Instance.bowlController.colorOfPaste;
            item.SetColor("_EmissionColor", item.color);
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


    public void EnableTakeOffMechanics()
    {
        if (!mask.GetComponent<MaskPaste>().maskIsPastingComplete)
            return;

        if (masktakingStarted)
            return;


        takingMaskOff?.gameObject.SetActive(true);
        mask?.gameObject.SetActive(false);
        takingMaskOff.Play("Take001");
        masktakingStarted = true;
    }

    float masktakingOffThreshold = 0;


    public void MasktakingOff()
    {
        if (!mask.GetComponent<MaskPaste>().maskIsPastingComplete)
            return;
        if (!masktakingStarted)
            return;

        float Mouse_Y = Mathf.Clamp(Input.GetAxis("Mouse Y"),-Mathf.Infinity,0);
       
        if (Mouse_Y!=0 && Input.GetMouseButton(0))
        {

            masktakingOffThreshold = Mathf.Clamp(Mouse_Y * 100, 0, 1);
            takingMaskOff.SetFloat("Speed", masktakingOffThreshold);


        }
        else
        {
           
            takingMaskOff.SetFloat("Speed", 0);

        }





    }






}
[Serializable]
public class MaskMakingTasks
{
    public bool Started = false;
    public bool Completed=false;
}