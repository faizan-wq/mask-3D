using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Unity.Jobs;
using Unity.Burst;

public class MaskMakingLevel : MonoBehaviour
{


    public static MaskMakingLevel Instance;
    public Mask_Making_Level_Type maskiMakingType;

    [SerializeField] private Mask_Making_Level_Methods currentMethod;
    [HideInInspector]public SoundManager soundManager;


    [Header("Camera")]
    public Camera camera;
    public ChangeCameraPositionTest changeCameraPositionTest; 
    [SerializeField] private List<Transform> cameraPositions;
   
    [Header("Animators")]
    public Animator Board;


    public List<RectTransform> iconList;
    public Image progressBarParent;
    public GameObject progressPanel;

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

    [Header("Syringe")]
    public SyringeController syringeController;


    [Header("Mask making machine")]
    public MaskMakingController maskMakingController;

    [Header("Mask Applying")]
    public MaskApplyingController maskApplyingController;


    #region Unity
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

       if( PlayerPrefs.GetString("Scene")=="1")
        {
            gameObject.SetActive(false);
        }
        foreach (var item in iconList)
        {
            item.gameObject.SetActive(true);
            item.GetChild(0).localScale = Vector3.one * 0.7f;
        }
        progressPanel.SetActive(true);

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

        if (syringeController == null)
            syringeController = GameObject.FindAnyObjectByType<SyringeController>();

        if (maskMakingController == null)
            maskMakingController = GameObject.FindAnyObjectByType<MaskMakingController>();

        if (maskApplyingController == null)
            maskApplyingController = GameObject.FindAnyObjectByType<MaskApplyingController>();

        soundManager = ParticleManager.Instance.soundManager;


        //NextBtnChopping();
       
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



    public void EnableTaskPoint(int number,float value)
    {
        ParticleManager.Instance.soundManager.PlayQuickSoundClip("task complete");
        ParticleManager.Instance.soundManager.PlayVibration("0,100,5,100");
        iconList[number].transform.GetChild(0).gameObject.SetActive(true);
        progressBarParent.rectTransform.GetChild(0).GetComponent<Image>().fillAmount = value;
        if(value==1)
        {
            iconList[number].transform.GetChild(0).localScale = Vector3.one;
        }

    }
   




    public void NextMethod(Mask_Making_Level_Methods method)
    {


        ParticleManager.Instance.PlayAnimation("Happy_Emoji");
        currentMethod = method;


    }

    private void CurrentMethodOperations()
    {

        if(maskiMakingType== Mask_Making_Level_Type.Chopping)
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
                    InjectingMethod();
                    break;
                case Mask_Making_Level_Methods.Mask_Making:
                    MaskMakingMethod();
                    break;
                case Mask_Making_Level_Methods.Mask_Applying:
                    MaskApplyingMethod();
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (currentMethod)
            {
                //case Mask_Making_Level_Methods.Chopping:
                //    ChoppingMethod();
                //    break;
                case Mask_Making_Level_Methods.Hammering:
                    HammeringMethod();
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
                    InjectingMethod();
                    break;
                case Mask_Making_Level_Methods.Mask_Making:
                    MaskMakingMethod();
                    break;
                case Mask_Making_Level_Methods.Mask_Applying:
                    MaskApplyingMethod();
                    break;
                default:
                    break;
            }
        }
      
    }






    #region Knife Methods
   
    private void ChoppingMethod()
    {
        if (knifeController.animator.GetBool("Chopping"))
        {
            if (Input.GetMouseButton(0))
            {
                knifeController.TutorialScreen2.SetActive(true);
                knifeController.KnifeMovementWithLerp(knifeController.knifeStartingPosition.position, knifeController.knifeEndingPosition.position, knifeController.knifeMovingSpeed);
                knifeController.KnifeChoppingSpeed(1);
                
            }
            else
            {
                knifeController.KnifeChoppingSpeed(0);
                knifeController.TutorialScreen2.SetActive(true);
            }
        }
       
          
    }


    public void NextBtnChopping()
    {

        //LevelUIManager.Instance.next.gameObject.SetActive(true);
        //LevelUIManager.Instance.next.onClick.AddListener(() => {
        //    NextMethod(Mask_Making_Level_Methods.MoveToCrushing);
      

        //});
    }

    #endregion

    #region Move to Crushing Methods
    private bool crushingOnlyOnce;

    private void MoveToCrushingMethod()
    {
       if(!crushingOnlyOnce)
        {
            MaskMakingLevel.Instance.EnableTaskPoint(1, 0);
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
            soundManager.PlayQuickSoundClip("FRUIT DROP");

            UnParentObjectsInsideBoard();
            hammerController.GetTheHammer();
            Board.speed = -1f;
            Board.Rebind();
            ParticleManager.Instance.PlayAnimation("Star Effect", Vector3.zero);
            NextMethod(Mask_Making_Level_Methods.Crushing);
            LevelUIManager.Instance.NextScreen(currentMethod);
           

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
            EnableTaskPoint(2, 0);
            
            bottleController.Tutorial1.SetActive(true);
            LevelUIManager.Instance.NextScreen(Mask_Making_Level_Methods.Pouring);
            bottleController.PouringMethodCalled = true;
           
        }
    }

    #endregion


    #region Mixing

    [HideInInspector] public bool mixingComplete;

    float Mouse_X=0;
    float Mouse_Y=0;
    private void MixingMethod()
    {

        if (mixingComplete)
            return;

        if(!hammerController.isMixingProcessPlayed)
        {
            MaskMakingLevel.Instance.EnableTaskPoint(3, 0);
            bottleController.BottleBackToStartingPosition();
            hammerController.HammerMovesToMixingPosition();

            hammerController.isMixingProcessPlayed = true;
            return;
        }
        Mouse_X = Mathf.Clamp(Mathf.Abs(Input.GetAxis("Mouse X")), 0, 0.25f);
        Mouse_Y = Mathf.Clamp(Mathf.Abs(Input.GetAxis("Mouse Y")), 0, 0.25f);

        if ((Mouse_X > 0 || Mouse_Y >0 )&& Input.GetMouseButton(0))
        {
           
            
            hammerController.HammerRotationEffect((Mouse_X + Mouse_Y) * Time.deltaTime * 30);
            ParticleManager.Instance.soundManager.PlayCompleteSoundClip("bowl mixing", true);
            bowlController.UpdateColorChangingEffect();
            //if(!mixingParticlelPlayed)
            //{
            //    StartCoroutine(ParticleEffect(() =>
            //    {
            //        ParticleManager.Instance.PlayAnimation("Mixing Item", hammerController.transform.position + Vector3.up * 3, bowlController.colorOfPaste);
            //    }, 0.25f));
            //}
          

        }
       else
        {
            hammerController.HammerRotationEffect(0);
            ParticleManager.Instance.soundManager.PlayCompleteSoundClip("bowl mixing", false);
        }

       if(bowlController.checkColorChangedCompletely)
        {
            mixingComplete = true;

            ParticleManager.Instance.soundManager.PlayCompleteSoundClip("bowl mixing", false);
            hammerController.ResetToStartingPosition();
        }




    }

    private bool mixingParticlelPlayed;
    IEnumerator ParticleEffect(Action a,float wait)
    {
        mixingParticlelPlayed = true;
        a?.Invoke();
        yield return new WaitForSeconds(wait);
        mixingParticlelPlayed = false;


    }







    #endregion

    #region Injection

    private bool injectingMethodPlayOnce;



    private void InjectingMethod()
    {
        if(!injectingMethodPlayOnce)
        {
            syringeController.UpdateColorOfSyringePasteAndParticles();
            syringeController.InjectionMoveToBowlPosition();

            injectingMethodPlayOnce = true;
            MaskMakingLevel.Instance.EnableTaskPoint(4, 0);
            return;
        }

        if (!syringeController.tabletMechanicsStarted)
        {



            if (Input.GetMouseButton(0))
            {
                syringeController.InjectionPushOrPull(true);
               
            }
            else
            {
                syringeController.InjectionPushOrPull(false);
               
            }



            syringeController.SyringePouredInMachineComplete();

            syringeController.InjectionMethodScenario();


          
        }
        else
        {
           
            syringeController.MoveTabletMethodIitiate();
        }
    }




    #endregion


    #region Mask Making


    

    private void MaskMakingMethod()
    {
        
        maskMakingController.MachineShaking();
        maskMakingController.EnableTakeOffMechanics();
        maskMakingController.MasktakingOff();

    }



    #endregion


    #region Apply On Face




    private void MaskApplyingMethod()
    {
        maskApplyingController.ApplyOnUpdate();
        maskApplyingController.ApplyOnStart();
    }




    #endregion

    #region Hammering Level

    private bool allowCreateNewHammeingItem = false;
    private bool hammeingProcessStart;
    private bool hammeringProcessFinish=false;
    [HideInInspector]public int hammeringItemLimit;
    int hammeringCreatedItemCount;





    private void HammeringMethod()
    {
        if(!hammeingProcessStart)
        {
            MaskMakingLevel.Instance.EnableTaskPoint(0, 0);
            LevelUIManager.Instance.NextScreen(currentMethod);
            hammerController.SelectHammeringTutorial(0, true);
            hammeingProcessStart = true;
        }

       


        if (hammeringProcessFinish)
        {
            
            hammerController.GetTheHammer();
            NextMethod(Mask_Making_Level_Methods.Crushing);
            MaskMakingLevel.Instance.EnableTaskPoint(0, 1);

            hammeringProcessFinish = false;
            
        }
       


        if(Input.GetMouseButtonDown(0))
        {
           
            if (CreateHammeringItem())
            {
               
                Debug.Log("Hammering Object Created");
            }
            else
            {
               
              
               
                Debug.Log("Limit is Full");
            }
        }
       

        
    }

    private  bool CreateHammeringItem()
    {
        if (hammeringCreatedItemCount >= hammeringItemLimit)
            return false;

        InstatitateObject(ItemsManager.Instance.selectedItem.prefab, hammerController.firstHammerPosition.position);

        hammeringCreatedItemCount++;
        if(hammeringCreatedItemCount==hammeringItemLimit)
        {
            hammeringProcessFinish = true;
            hammerController.SelectHammeringTutorial(1, false);
            Debug.Log("All Hammering Objects are created");
        }
        return true;
    }

    private void InstatitateObject(GameObject obj, Vector3 startingPosition)
    {
        GameObject temp = Instantiate(obj);

        obj.transform.position = startingPosition;
    }

    #endregion


    #endregion


}

public enum Mask_Making_Level_Methods
{
    Chopping,
    MoveToCrushing,
    Hammering,
    Crushing,
    Pouring,
    Mixing,
    Injecting,
    Mask_Making,
    Mask_Applying
}
public enum Mask_Making_Level_Type
{
    Chopping,
    Hammer
}