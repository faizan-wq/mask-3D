using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMakingLevel : MonoBehaviour
{


    public static MaskMakingLevel Instance;

    [Header("Camera")]
    [SerializeField] private Camera camera;
    [SerializeField] private List<Transform> cameraPositions;
   
    [Header("Animators")]
    [SerializeField] private Animator knife;
    public Animator Board;

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





    #region Unity
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    private void Update()
    {
        if(knife.GetBool("Chopping"))
        if(Input.GetMouseButton(0))
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



    #region Methods




    public void KnifeMoveToChoppingPosition()
    {
        knife.SetBool("StartingPosition",true);
        Invoke(nameof(KnifeStartChopping), knifeWaitChoppingPosition);
    }
    private void KnifeStartChopping()
    {

        knife.SetBool("Chopping", true);

    }    

    
    
    public void KnifeChoppingSpeed(float speed)
    {
       
        if(Vector3.Distance(knife.transform.position,knifeEndingPosition.position)<=0.125f)
        {
           
            knife.SetFloat("ChoppingSpeed", 0);
            return;
        }
        knife.SetFloat("ChoppingSpeed",speed);
    }

    float value;
    private void KnifeMovementWithLerp(Vector3 starting, Vector3 ending, float speed)
    {
        if(Vector3.Distance(knife.transform.position,ending)<0.125f)
        {
            return;
        }
        value += speed * Time.deltaTime;
        
        Vector3 pos = Vector3.MoveTowards(starting,ending,value);
        knife.transform.position = pos;
      // GameObject.FindAnyObjectByType<GamePlayScene>().gameObject.SetActive(s)
          
    }



    #endregion
















}
