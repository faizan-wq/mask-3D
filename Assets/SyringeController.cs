using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SyringeController : MonoBehaviour
{


    #region Injecting



    public Animator Syringe;
    [SerializeField] private Transform intoTheBowl;
    [SerializeField] private Transform intoTheMachine;
    [SerializeField] private ParticleSystem syringeParticles;
    private const string pushSyringe="Push";
    private const string pullSyringe= "Pull";
    private const string speedSyringe = "Speed";
    private bool allowFillingOrRefillingInjection;
    private bool syringePouringComplete;
    private bool onlyOnce;

    [HideInInspector]public bool tabletMechanicsStarted ;


       

    #region Methods


    public void InjectionMoveToBowlPosition()
    {
        Action action = () => {
            SetAnimatorParameter(pullSyringe,true);
            allowFillingOrRefillingInjection = true;
          //  Invoke(nameof(InjectionMoveToMachinePosition),2);

        };
        MoveToTargetedPosition(Syringe.transform, intoTheBowl,1, action);
        
    }
    public void InjectionMoveToMachinePosition()
    {
        Action action = () => {
            SetAnimatorParameter(pushSyringe, true);
            allowFillingOrRefillingInjection = true;


        };
        allowFillingOrRefillingInjection = false;
        MoveToTargetedPosition(Syringe.transform, intoTheMachine, 1, action);
        MaskMakingLevel.Instance.changeCameraPositionTest.ChangeTrack(2);
    }

    public void InjectionMethodScenario()
    { 
       if(Syringe.GetComponent<Syringe>().SyringPullCheck)
        {
            if(!onlyOnce)
            {
                Invoke(nameof(InjectionMoveToMachinePosition), 2);
                onlyOnce = true;
            }

           
        }
      
    }

    public void SyringePouredInMachineComplete()
    {
        if(Syringe.GetComponent<Syringe>().SyringPushCheck)
        {
            allowFillingOrRefillingInjection = false;
            tabletMechanicsStarted = true;
            Syringe.gameObject.SetActive(false);
            Debug.Log("Syringe Mechanics Complete Now tablet method");
        }
    }



    public void InjectionPushOrPull(bool check)
    {
        if (!allowFillingOrRefillingInjection)
        {
            EnableSyringeLiquidParticles(false);
            return;
        }
          

        if(check)
        {
            SetAnimatorParameter(speedSyringe,1f);
        }
        else
        {
            SetAnimatorParameter(speedSyringe, 0f);
        }

       if(Syringe.GetComponent<Syringe>().SyringPullCheck)
            EnableSyringeLiquidParticles(check);


    }




    private void MoveToTargetedPosition(Transform trans, Transform pos, float time,Action action=null)
    {
        trans.DOMove(pos.position, time).OnComplete(()=> {

            action?.Invoke();
            Debug.Log(trans.name + " Reached Position " + pos.name);
        });
    }

    private void SetAnimatorParameter(string boolName, bool check)
    {
        Syringe.SetBool(boolName, check);
    }

    private void SetAnimatorParameter(string boolName, float value)
    {
        Syringe.SetFloat(boolName, value);
    }
    private void EnableSyringeLiquidParticles(bool check)
    {
        syringeParticles.gameObject.SetActive(check);
        if (check)
        {
           
            syringeParticles.Play();
        }
        else
        {
           
            syringeParticles.Stop();
        }
        

    }
    #endregion







    #endregion





}
