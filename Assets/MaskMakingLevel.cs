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
    [SerializeField] private Animator bowl;
    [SerializeField] private Animator maskMaker;
    [SerializeField] private Animator character;
    [Header("Items")]
    public Transform choppingItemPosition;



    [Header("Knife")]
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
    #endregion



    #region Methods









    private void KnifeMovement(Vector3 starting, Vector3 ending, float speed)
    {
        float value = Mathf.Clamp(starting.x + speed*Time.deltaTime, 0, 1);
        Vector3 pos = Vector3.Lerp(starting,ending,value);
        knife.transform.position = pos;
      // GameObject.FindAnyObjectByType<GamePlayScene>().gameObject.SetActive(s)
          
    }



    #endregion
















}
