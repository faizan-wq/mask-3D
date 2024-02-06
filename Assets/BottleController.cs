using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottleController : MonoBehaviour
{

    public bool PouringMethodCalled;
    public List<Bottle> bottles;




    public Transform bottleStarting;
    private Vector3 resetPosition;




    public Transform WaterShader;
    private int bottleSelected = -1;
    [HideInInspector] public Bottle selectedBottle;
    [SerializeField] private float bottleStartingPositionTime;
    public GameObject Tutorial1;
    public GameObject Tutorial2;

    private bool bottleReachedStartingPosition;
    private float timer;
    private float waterRise= -1.4f;
   
    public bool pouringInBowlIsComplete;
    private bool ResetBottlePosition;
    
    
    #region UNITY


    private void Start()
    {
        
        resetPosition = transform.position;
    }

    private void Update()
    {
        if (ResetBottlePosition)
            return;
        

        if (!bottleReachedStartingPosition)
            return;


        if(Input.GetMouseButton(0) && !pouringInBowlIsComplete)
        {
            Tutorial2.SetActive(false);
            PouringWaterIntheBowl(true);

        }
        else
        {
            Tutorial2.SetActive(true);
            PouringWaterIntheBowl(false);
        }
    }

    #endregion



    #region METHODS

    public void  SelectedBottle(int number)
    {
        Tutorial1.SetActive(false);
        bottleSelected = number;
        EnableSelectedBottle();

        MaskMakingLevel.Instance.bowlController.startAnimation = true;
    }

    public void EnableSelectedBottle()
    {
        if (bottleSelected == -1)
        {
            int number = UnityEngine.Random.Range(0, bottles.Count);
            SelectedBottle(number);

        }
        selectedBottle = bottles[bottleSelected];
        MaskMakingLevel.Instance.bowlController.ChangeColorOfwater(selectedBottle.color);
        bottles[bottleSelected].bottle.SetActive(true);
        resetPosition = selectedBottle.bottle.transform.position; 
        BottleMoveToPosition();
    }


    private void BottleMoveToPosition()
    {
        selectedBottle.bottle.transform.DOMove(bottleStarting.position, bottleStartingPositionTime).OnComplete(()=> {


            bottleReachedStartingPosition = true;


        });
    }


    


    private void PouringWaterIntheBowl(bool check)
    {
        if(check)
        {
            BottleRotating(Quaternion.Euler(Vector3.zero), bottleStarting.localRotation, 1);
        }
        else
        {
            BottleRotating(Quaternion.Euler(Vector3.zero), bottleStarting.localRotation, -1);
        }
    }


    private void  BottleRotating(Quaternion starting, Quaternion ending, float speed)
    {
        timer =Mathf.Clamp(timer+Time.deltaTime * speed,0,1);
        selectedBottle.bottle.transform.localRotation = Quaternion.Lerp(starting,ending,timer);
        ParticleSystem(timer);
    }

    private void WaterRising()
    {
        waterRise = Mathf.Clamp(waterRise + Time.deltaTime, -2.13f, 3.21f);
        Vector3 pos = WaterShader.localPosition;
        pos.y = waterRise;
        WaterShader.localPosition = pos;
        if(waterRise>= 3.21f)
        {
            pouringInBowlIsComplete = true;
        }
        
     

    }

    private void ParticleSystem(float value)
    {
        if(value>=0.75f)
        {
            selectedBottle.bottle.transform.GetChild(1).gameObject.SetActive(true);
            WaterRising();
           
        }
        else
        {
            selectedBottle.bottle.transform.GetChild(1).gameObject.SetActive(false);
            if(value<=0)
            {
                if(pouringInBowlIsComplete)
                {
                    ResetBottlePosition = true;
                    Tutorial2.SetActive(false);
                    Tutorial1.SetActive(false);
                    MaskMakingLevel.Instance.NextMethod(Mask_Making_Level_Methods.Mixing);
                }
                
            }
        }
    }





    public void BottleBackToStartingPosition()
    {
        
             selectedBottle.bottle.transform.DOMove(resetPosition, bottleStartingPositionTime).OnComplete(() => {


                
                 selectedBottle.bottle.SetActive(false);


             });
    }







    #endregion




}
[Serializable]
public class Bottle
{

    public GameObject bottle;
    public Color color;



}