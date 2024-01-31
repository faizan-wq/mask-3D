using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
public class MaskMakingLevel : MonoBehaviour
{


    public static MaskMakingLevel Instance;
    [SerializeField] private Mask_Making_Level_Methods currentMethod;
    [Header("Camera")]
    [SerializeField] private Camera camera;
    [SerializeField] private ChangeCameraPositionTest changeCameraPositionTest; 
    [SerializeField] private List<Transform> cameraPositions;
   
    [Header("Animators")]
    public Animator Board;
   
    [SerializeField] private Animator bowl;
    [SerializeField] private Animator maskMaker;
    [SerializeField] private Animator character;
    [Header("Items")]
    public Transform choppingItemPosition;


    [Header("Knife")]
    public KnifeController knifeController;
   

    [Header("Hammer")]
    public HammerController hammerController;

    [Header("Bottle")]
    public BottleController bottleController;

    [Header("Bowl")]
    public BowlController bowlController;



    #region Unity
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        if(knifeController == null)
            knifeController = GameObject.FindAnyObjectByType<KnifeController>();

        if (hammerController == null)
            hammerController = GameObject.FindAnyObjectByType<HammerController>();
        
        if (bottleController == null)
            bottleController = GameObject.FindAnyObjectByType<BottleController>();

        if (bowlController == null)
            bowlController = GameObject.FindAnyObjectByType<BowlController>();

        NextBtnChopping();
       
    }


    private void Update()
    {
        CurrentMethodOperations();
    }


    public void RemoveAllFunctionsFromNext()
    {

    }











    #endregion



    #region Methods




    public void NextMethod(Mask_Making_Level_Methods method)
    {



        currentMethod = method;


    }

    private void CurrentMethodOperations()
    {
        switch (currentMethod)
        {
            case Mask_Making_Level_Methods.Chopping:
                ChoppingMethod();
                break;
            case Mask_Making_Level_Methods.MoveToCrushing:
                MoveToCrushingMethod();
                break;
            case Mask_Making_Level_Methods.Crushing:
                CrushingMethod();
                break;
            case Mask_Making_Level_Methods.Pouring:
                PouringMethod();
                break;
            case Mask_Making_Level_Methods.Mixing:
                MixingMethod();
                break;
            case Mask_Making_Level_Methods.Injecting:
                break;
            case Mask_Making_Level_Methods.Mask_Making:
                break;
            case Mask_Making_Level_Methods.Mask_Applying:
                break;
            default:
                break;
        }
    }






    #region Knife Methods
   
    private void ChoppingMethod()
    {
        if (knifeController.animator.GetBool("Chopping"))
            if (Input.GetMouseButton(0))
            {
                knifeController.KnifeMovementWithLerp(knifeController.knifeStartingPosition.position, knifeController.knifeEndingPosition.position, knifeController.knifeMovingSpeed);
                knifeController.KnifeChoppingSpeed(1);
            }
            else
            {
                knifeController.KnifeChoppingSpeed(0);
            }
    }


    public void NextBtnChopping()
    {

        LevelUIManager.Instance.next.gameObject.SetActive(true);
        LevelUIManager.Instance.next.onClick.AddListener(() => {
            NextMethod(Mask_Making_Level_Methods.MoveToCrushing);
            Debug.Log("Next Button Is pressed");

        });
    }

    #endregion

    #region Move to Crushing Methods
    private bool crushingOnlyOnce;

    private void MoveToCrushingMethod()
    {
       if(!crushingOnlyOnce)
        {
            knifeController.animator.SetBool("Chopping", false);
            knifeController.gameObject.SetActive(false);
            knifeController.knife.DisableKinematicsOfItemSlice();
            BoardStartMoving();
            NextBtnMoveToCrushing();
            crushingOnlyOnce = true;
          
        }
        

    }

    private void BoardStartMoving()
    {
        Board.enabled=true;
        Board.SetBool("PlaceInBowl", true);
        Board.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
       
        //Invoke(nameof(ChangeBoardKnife), 2);
        ChangeBoardKnife();
        changeCameraPositionTest.ChangeTrack(1);



    }

    private void UnParentObjectsInsideBoard()
    {
        Transform obj = Board.transform.GetChild(0).GetChild(1).transform;
        for (int i = obj.childCount-1; i >=0 ; i--)
        {
            obj.GetChild(i).SetParent(null);
        }
    }

    private void ChangeBoardKnife()
    {
        Board.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        Board.transform.GetChild(0).transform.GetChild(0).DOLocalMove(new Vector3(0.0902f, 0.05301232f, 0.0352935f),2.5f).SetDelay(2).OnComplete(()=> {

            UnParentObjectsInsideBoard();
            hammerController.GetTheHammer();
            Board.speed = -1f;
            Board.Rebind();
            NextMethod(Mask_Making_Level_Methods.Crushing);
            LevelUIManager.Instance.NextScreen(currentMethod);
            Debug.Log("ChangeBoardKnife Completed");



        });

       
    }
    
   


    public void NextBtnMoveToCrushing()
    {

        LevelUIManager.Instance.next.gameObject.SetActive(false);

        LevelUIManager.Instance.next.onClick.RemoveAllListeners();
           
    }




    #endregion

    #region Crushing Methods
  
    private void CrushingMethod()
    {
        if (hammerController.DoNotAllowCrushing)
            return;

        hammerController.GetHammerCrush();
    }

 
    public void NextBtnCrushing()
    {

        LevelUIManager.Instance.next.gameObject.SetActive(true);

        LevelUIManager.Instance.next.onClick.AddListener(
            ()=> { 
            
            
            });

    }

    #endregion


    #region Pouring


    private void PouringMethod()
    {
      if(!bottleController.PouringMethodCalled)
        {
            ItemsManager.Instance.CreateliquidItems();
            LevelUIManager.Instance.NextScreen(Mask_Making_Level_Methods.Pouring);
            bottleController.PouringMethodCalled = true;
            Debug.Log("Pouring Methods is Called");
        }
    }

    #endregion


    #region Mixing

    [HideInInspector] public bool mixingComplete;


    private void MixingMethod()
    {

        if (mixingComplete)
            return;

        if(!hammerController.isMixingProcessPlayed)
        {
            bottleController.BottleBackToStartingPosition();
            hammerController.HammerMovesToMixingPosition();

            hammerController.isMixingProcessPlayed = true;
            return;
        }

       if(Input.GetAxis("Mouse X")>=0.005f || Input.GetAxis("Mouse Y") >= 0.005f)
        {
            float Mouse_X = Mathf.Abs(Input.GetAxis("Mouse X"));
            float Mouse_Y = Mathf.Abs(Input.GetAxis("Mouse Y"));

            hammerController.HammerRotationEffect((Mouse_X + Mouse_Y) * Time.deltaTime * 5);
            bowlController.UpdateColorChangingEffect();

        }
       else
        {
            hammerController.HammerRotationEffect(0);
        }

       if(bowlController.checkColorChangedCompletely)
        {
            mixingComplete = true;
           
           
            hammerController.ResetToStartingPosition();
        }




    }









    #endregion



    #endregion
















}

public enum Mask_Making_Level_Methods
{
    Chopping,
    MoveToCrushing,
    Crushing,
    Pouring,
    Mixing,
    Injecting,
    Mask_Making,
    Mask_Applying
}
