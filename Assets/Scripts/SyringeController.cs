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
    [SerializeField] private List<MeshRenderer> syringeLiquid;
    [SerializeField] private GameObject Tutorial;

    private const string pushSyringe="Push";
    private const string pullSyringe= "Pull";
    private const string speedSyringe = "Speed";
    private bool allowFillingOrRefillingInjection;
    private bool syringePouringComplete;
    private bool onlyOnce;


    [HideInInspector]public bool tabletMechanicsStarted ;


       

    #region Methods

    public void UpdateColorOfSyringePasteAndParticles()
    {
        foreach (var item in syringeLiquid)
        {
            foreach (var material in item.materials)
            {
                material.color = MaskMakingLevel.Instance.bowlController.colorOfPaste;
            }
        }


        foreach (var material in syringeParticles.GetComponent<ParticleSystemRenderer>().materials)
        {
            material.color = MaskMakingLevel.Instance.bowlController.colorOfPaste;
        }
       
    }



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
                Invoke(nameof(InjectionMoveToMachinePosition), 0);
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
            Tutorial.SetActive(false);
            Invoke(nameof(DisableSyringe), 0.25f);
          
         
        }
    }

    private void DisableSyringe()
    {
        MaskMakingLevel.Instance.EnableTaskPoint(4, 1);
        Syringe.gameObject.SetActive(false);
        allowtabletMechanics = true;
        tablet.gameObject.SetActive(true);
        Invoke(nameof(EnableTabletTutorial),1f);
        
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
            Tutorial.SetActive(false);
            SetAnimatorParameter(speedSyringe,0.25f);
        }
        else
        {
            Tutorial.SetActive(true);
            SetAnimatorParameter(speedSyringe, 0f);
        }
        MaskMakingLevel.Instance.progressBarParent.gameObject.SetActive(false);
        if (Syringe.GetComponent<Syringe>().SyringPullCheck)
            EnableSyringeLiquidParticles(check);


    }




    private void MoveToTargetedPosition(Transform trans, Transform pos, float time,Action action=null)
    {
        trans.DOMove(pos.position, time).OnComplete(()=> {

            action?.Invoke();
      
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

    #region Tablet
    private const string Tablet_Header = "Tablet";

    [Header(Tablet_Header)]
    [SerializeField] private Animator tablet;
    [SerializeField] private ParticleSystem tabletParticle;
    public GameObject Tutorial2;
    private const string tabletInitiate = "Initiate";
    private bool tabletReachedPosition;
    private bool tabletMethodOnce;
    private bool allowtabletMechanics;
    private bool starttabletEffect;

    public void MoveTabletMethodIitiate()
    {
        if (!allowtabletMechanics)
            return;


        if(!starttabletEffect)
            if(Input.GetAxis("Mouse Y")>0.5f && Input.GetMouseButton(0))
            {
                starttabletEffect = true;
                Tutorial2.SetActive(false);
            }
        if(starttabletEffect)
        {
            if (!tabletMethodOnce)
            {
                MaskMakingLevel.Instance.EnableTaskPoint(5, 0);
                ApplyAnimationOftablet();
                tabletMethodOnce = true;
            }
            if (tablet.GetComponent<Tablet>().tabletParticles)
                tabletParticle.gameObject.SetActive(true);


            if (!tabletReachedPosition)
            {
                ChecktabletReachedDestination();
            }
        }



        
    
    }
    private void EnableTabletTutorial()
    {
        Tutorial2.SetActive(true);
    }
    private void ChecktabletReachedDestination()
    {
       
        if (tablet.GetComponent<Tablet>().tabletInsideMachne)
        {
            Debug.Log("MaskMakingMethod");
            MaskMakingLevel.Instance.NextMethod(Mask_Making_Level_Methods.Mask_Making);
            tabletReachedPosition = true;
            tablet.GetComponent<Tablet>().tabletParticles = false;
            tabletParticle.gameObject.SetActive(false);
            tablet.gameObject.SetActive(false);
            allowtabletMechanics = false;
            tablet.GetComponent<Tablet>().tabletInsideMachne = false;


        }
        


    }

    private void ApplyAnimationOftablet()
    {
        
        tablet.SetBool(tabletInitiate, true);
    }



    #endregion




}
