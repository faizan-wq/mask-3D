using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class HammerController : MonoBehaviour
{
    private Animator animator;
    [HideInInspector] public MaskMakingLevel maskMakingLevel;

    private void Start()
    {
        maskMakingLevel = MaskMakingLevel.Instance;
        animator = GetComponent<Animator>();
        hammerStartingPosition = transform.position;
       SetHammerCrushingPositions();
    }

    #region Hammering
    [Header("Hammering")]
    public GameObject hammeringSelectionTutorial;
    public GameObject hammeringTapTutorial;


    public void SelectHammeringTutorial(int num = 0, bool check = false)
    {
        if(num==0)
            hammeringSelectionTutorial.SetActive(check);
        else if(num==1)
            hammeringTapTutorial.SetActive(check);

    }

    #endregion


    #region Crushing

    [Header("Crushing")]

    public Vector3 hammerStartingPosition;
    public Transform firstHammerPosition;
    public List<Transform> hammerCrusingPsitions;
    public int hammerCrushedCountLimit = 5;
    public int hammerCrushedCount;
    public bool DoNotAllowCrushing = true;
    public GameObject CrushingTutorial;
    public void GetHammerCrush()
    {
        if (Input.GetMouseButton(0))
        {
            CrushingTutorial.SetActive(false);
            CrushByhammer();
            DoNotAllowCrushing = true;
        }
        else
        {
            CrushingTutorial.SetActive(true);
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
        transform.position = randomPosition;
        StartCoroutine(ParticlePlayAfterWait(() => {
            ParticleManager.Instance.PlayAnimation("Crushing Item", randomPosition - Vector3.up * 8, ItemsManager.Instance.selectedItem.color);
            maskMakingLevel.soundManager.PlayQuickSoundClip("bowl hammer hit");
            maskMakingLevel.soundManager.PlayVibration("0,100,5,100");


        }, 0.2f));
       
        transform.DOMove(randomPosition - Vector3.up * 8, 0.6f).SetEase(Ease.OutBounce).OnComplete(() => {

            UpdateHammerCrushedCount();
           
            if (!HammerResetPosition())
            {
               
                DoNotAllowCrushing = false;
            }
               
            else
            {
                transform.position = randomPosition;
                transform.DOJump(hammerStartingPosition, 5, 1, 0.5f).OnComplete(()=>
                {
                    CrushingTutorial.SetActive(false);
                    maskMakingLevel.NextMethod(Mask_Making_Level_Methods.Pouring);
                 
                }
                );
               
               
                
            }

        });
    }
   
    private IEnumerator ParticlePlayAfterWait(Action a, float wait)
    {
        yield return new WaitForSeconds(wait);
        a?.Invoke();
    }

    Transform position;
    private Vector3 GetHammerCrushingPositionTransform()
    {
        Transform[] array = new Transform[hammerCrusingPsitions.Count - 1];
        int num = 0;
        for (int i = 0; i < hammerCrusingPsitions.Count; i++)
        {
            if (position != null)
            {
                if (position == hammerCrusingPsitions[i])
                {
                    continue;
                }
            }
            array[num] = hammerCrusingPsitions[i];
        }


        //int trans = Random.Range(0, hammerCrusingPsitions.Count);
        int trans = 1;
        position = hammerCrusingPsitions[trans];
        Array.Clear(array, 0, array.Length);
        return hammerCrusingPsitions[trans].position;

    }





    private void UpdateHammerCrushedCount(int number = 1)
    {
        hammerCrushedCount = Mathf.Clamp(hammerCrushedCount + number, 0, hammerCrushedCountLimit);
    }

    private bool HammerResetPosition()
    {
        maskMakingLevel.EnableTaskPoint(1, (float)hammerCrushedCount / hammerCrushedCountLimit);
        if (hammerCrushedCount < hammerCrushedCountLimit)
        {
         
            return false;
        }

    
        return true;
    }
    public void GetTheHammer()
    {
        transform.DOJump(firstHammerPosition.position, 1, 1, 1).OnComplete(() => {

            
            DoNotAllowCrushing = false;


        });
    }

    #endregion

    #region Mixing
    [Header("Mixing")]
    private const string Mixing_header="Mixing Process";
    private const string Hammer_Rotating = "Rotating";
    private const string Hammer_Speed = "Speed";

    [Header(Mixing_header)]
    public bool isMixingProcessPlayed;
    [SerializeField] private Transform MixingStartingPosition;
    [SerializeField] private Collider hammerModelCollider;
    [SerializeField] private GameObject RotatingTutorial;

    public void HammerMovesToMixingPosition()
    {
        transform.DOJump(MixingStartingPosition.position, 1, 1, 1).OnComplete(() => {


          
            MoveInsideRotatingPosition();

        });
    }

    public void ResetToStartingPosition()
    {
        
        SetHammerRotation(false);
        SetHammerModelCollider(false);
        RotatingTutorial.SetActive(false);
        transform.DOJump(hammerStartingPosition, 5, 1, 1).OnComplete(() => {

            maskMakingLevel.EnableTaskPoint(3, 1);
            maskMakingLevel.NextMethod(Mask_Making_Level_Methods.Injecting);
          
        });
    }



    private void MoveInsideRotatingPosition()
    {
        transform.DOJump(MixingStartingPosition.position-Vector3.up*8f, 1, 1, 1).OnComplete(() => {

            RotatingTutorial.SetActive(true);
            SetHammerRotation(true);
            SetHammerModelCollider(true);
          

        });
    }






    public void HammerRotationEffect( float value)
    {

        if (!GetHammerRotation())
            return;

        ChangeRotationSpeedOfHammer(value);
      

    }


    [HideInInspector]
    public void SetHammerRotation(bool check)
    {
        animator.SetBool(Hammer_Rotating, check);
        maskMakingLevel.soundManager.PlayCompleteSoundClip("bowl mixing", check);
    }

    private bool GetHammerRotation()
    {
        return animator.GetBool(Hammer_Rotating);
    }

    [HideInInspector]
    private void ChangeRotationSpeedOfHammer(float value)
    {

        animator.SetFloat(Hammer_Speed, value);

    }
    
    private void SetHammerModelCollider(bool check)
    {
        hammerModelCollider.enabled = check;
    }


   



    #endregion



}
