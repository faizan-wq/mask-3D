using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMakingLevel : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera camera;
    [SerializeField] private Transform knifePosition;
    [SerializeField] private Transform Position;
    [Header("Animators")]
    [SerializeField] private Animator knife;
    [SerializeField] private Animator bowl;
    [SerializeField] private Animator maskMaker;
    [SerializeField] private Animator character;
    [Header("Knife")]
    [SerializeField] private Transform knifeStartingPosition;
    [SerializeField] private Transform knifeEndingPosition;
    [SerializeField] private float knifeMovingSpeed=1;
    [SerializeField] private float knifeCuttingSpeed=1;
    [SerializeField] private float knifeDistance = 0;







    #region Methods

    private void KnifeMovement(Vector3 starting, Vector3 ending, float speed)
    {
        float value = Mathf.Clamp(knifeDistance + speed, 0, 1);
        Vector3 pos = Vector3.Lerp(starting,ending,value);
        knife.transform.position = pos;
       GameObject.FindAnyObjectByType<GamePlayScene>().gameObject.SetActive(s)
          
    }



    #endregion
















}
