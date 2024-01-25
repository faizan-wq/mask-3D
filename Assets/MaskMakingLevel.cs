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
    [SerializeField] private Animator knife;
    [SerializeField] private Animator bowl;
    [SerializeField] private Animator maskMaker;
    [SerializeField] private Animator character;
    [Header("Items")]
    public Transform choppingItemPosition;


    [Header("Knife")]
    [SerializeField] private float knifeWaitChoppingPosition = 1;
    [SerializeField] private Transform knifeStartingPosition;
    [SerializeField] private Transform knifeEndingPosition;
    [SerializeField] private float knifeMovingSpeed=1;
    [SerializeField] private float knifeCuttingSpeed=1;
    [SerializeField] private float knifeDistance = 0;
    [SerializeField] private bool HideAnimation;
    
    [Header("Hammer")]
    [SerializeField] private Transform hammer;
    [SerializeField] private Transform firstHammerPosition;
    [SerializeField] private List<Transform> hammerCrusingPsitions;



    #region Unity
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SetHammerCrushingPositions();
    }


    private void Update()
    {
        CurrentMethodOperations();
    }














    #endregion



    #region Methods




    public void NextMethod()
    {
       
        

        currentMethod = Mask_Making_Level_Methods.MoveToCrushing;

       
    }
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
    public void KnifeMoveToChoppingPosition()
    {
        knife.SetBool("StartingPosition", true);
        Invoke(nameof(KnifeStartChopping), knifeWaitChoppingPosition);
    }
    private void KnifeStartChopping()
    {

        knife.SetBool("Chopping", true);

    }
    public void KnifeChoppingSpeed(float speed)
    {

        if (Vector3.Distance(knife.transform.position, knifeEndingPosition.position) <= 0.125f)
        {

            knife.SetFloat("ChoppingSpeed", 0);
            return;
        }
        knife.SetFloat("ChoppingSpeed", speed);
    }
    float value;
    private void KnifeMovementWithLerp(Vector3 starting, Vector3 ending, float speed)
    {
        if (Vector3.Distance(knife.transform.position, ending) < 0.125f)
        {
            return;
        }
        value += speed * Time.deltaTime;

        Vector3 pos = Vector3.MoveTowards(starting, ending, value);
        knife.transform.position = pos;
        // GameObject.FindAnyObjectByType<GamePlayScene>().gameObject.SetActive(s)

    }

    private void ChoppingMethod()
    {
        if (knife.GetBool("Chopping"))
            if (Input.GetMouseButton(0))
            {
                KnifeMovementWithLerp(knifeStartingPosition.position, knifeEndingPosition.position, knifeMovingSpeed);
                KnifeChoppingSpeed(1);
            }
            else
            {
                KnifeChoppingSpeed(0);
            }
    }

    #endregion

    #region Move to Crushing Methods
    private bool crushingOnlyOnce;

    private void MoveToCrushingMethod()
    {
       if(!crushingOnlyOnce)
        {
            knife.SetBool("Chopping", false);
            knife.gameObject.SetActive(false);
            BoardStartMoving();
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
            GetTheHammer();
            Board.speed = -1f;
            Board.Rebind();
            NextMethod(Mask_Making_Level_Methods.Crushing);
            Debug.Log("ChangeBoardKnife Completed");



        });


    }
    
    private void GetTheHammer()
    {
        hammer.DOJump(firstHammerPosition.position,1,1,4).OnComplete(()=> {


            DoNotAllowCrushing = false;


        });
    }







    #endregion

    #region Crushing Methods
    private bool DoNotAllowCrushing = true;
    private void CrushingMethod()
    {
        if (DoNotAllowCrushing)
            return;

        GetHammerCrush();
    }

    private void GetHammerCrush()
    {
        if(Input.GetMouseButton(0))
        {
            CrushByhammer();
            DoNotAllowCrushing = true;
        }
       


    }



    private void SetHammerCrushingPositions()
    {
        foreach (var item in firstHammerPosition.GetComponentsInChildren<Transform>())
        {
            hammerCrusingPsitions.Add(item);
        }
    }
    private void CrushByhammer()
    {

        Vector3 randomPosition = GetHammerCrushingPositionTransform();
        hammer.position = randomPosition;
        hammer.DOMove(randomPosition - Vector3.up * 8, 1f).SetEase(Ease.OutBounce).OnComplete(()=> {

            DoNotAllowCrushing = false;


        });
    }


    Transform position;
    private Vector3 GetHammerCrushingPositionTransform()
    {
        Transform[] array = new Transform[hammerCrusingPsitions.Count-1];
        int num = 0;
        for (int i = 0; i < hammerCrusingPsitions.Count; i++)
        {
            if(position!=null)
            {
                if(position==hammerCrusingPsitions[i])
                {
                    continue;
                }
            }
            array[num] = hammerCrusingPsitions[i];
        }

        
        int trans = Random.Range(0, hammerCrusingPsitions.Count);
        position = hammerCrusingPsitions[trans];
        Array.Clear(array,0,array.Length);
        return hammerCrusingPsitions[trans].position;

    }

    #endregion




    #endregion
















}

public enum Mask_Making_Level_Methods
{
    Chopping,
    MoveToCrushing,
    Crushing,
    Injecting,
    Mask_Making,
    Mask_Applying
}
