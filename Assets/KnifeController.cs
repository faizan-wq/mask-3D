using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KnifeController : MonoBehaviour
{

    public float knifeWaitChoppingPosition = 1;
    public Transform knifeStartingPosition;
    public Transform knifeEndingPosition;
    public float knifeMovingSpeed = 1;
    public float knifeCuttingSpeed = 1;
    public float knifeDistance = 0;
    public bool HideAnimation;
    public GameObject TutorialScreen1;
    public GameObject TutorialScreen2;

    [HideInInspector]public Animator animator;
    [HideInInspector]public Knife knife;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        knife = transform.GetChild(0).GetComponent<Knife>();
        TutorialScreen1.SetActive(true);

    }

    public void KnifeMoveToChoppingPosition()
    {
        TutorialScreen1.SetActive(false);
        animator.SetBool("StartingPosition", true);
        animator.transform.position = knifeStartingPosition.position;
        MaskMakingLevel.Instance.EnableTaskPoint(0, 0);
        Invoke(nameof(KnifeStartChopping), knifeWaitChoppingPosition);
    }
    private void KnifeStartChopping()
    {
      
       
        animator.SetBool("Chopping", true);
      

    }
    public void KnifeChoppingSpeed(float speed)
    {

        if (Vector3.Distance(animator.transform.position, knifeEndingPosition.position) <= 0.125f)
        {
           

            animator.SetFloat("ChoppingSpeed", 0);
            return;
        }
        animator.SetFloat("ChoppingSpeed", speed);
    }
    float value;
    public void KnifeMovementWithLerp(Vector3 starting, Vector3 ending, float speed)
    {
        if (Vector3.Distance(animator.transform.position, ending) < 0.125f)
        {
            TutorialScreen1.SetActive(false);
            TutorialScreen2.SetActive(false);
            MaskMakingLevel.Instance.EnableTaskPoint(0, 1);
            MaskMakingLevel.Instance.NextMethod(Mask_Making_Level_Methods.MoveToCrushing);

            return;
        }
        value += speed * Time.deltaTime;
        Vector3 pos = Vector3.MoveTowards(starting, ending, value);
        animator.transform.position = pos;

        MaskMakingLevel.Instance.EnableTaskPoint(0,1- Vector3.Distance(animator.transform.position, ending)/10);

    }

}
