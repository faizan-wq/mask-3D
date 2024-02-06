using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskApplyingController : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> masksMesheRenderers;
    [SerializeField] private GameObject MaskToTarget;
    [SerializeField] private GameObject FinishUIPanel;

    [SerializeField] private Animator mask;
    [SerializeField] private Animator Character;

    [SerializeField] private GameObject maskOnFace;
    private bool maskReachedPositionOnce = false;
    private bool processComplete;



    private bool maskApplyingOnce;
    public void ApplyOnStart()
    {
        if (!maskApplyingOnce)
        {
            UpdateMaskRenderer();
            mask.gameObject.SetActive(true);
            MaskMakingLevel.Instance.changeCameraPositionTest.ChangeTrack(3);
            maskApplyingOnce = true;
        }
    }
    
    public void ApplyOnUpdate()
    {
        
        MoveToTargetPosition();
        ReachedTargetOnce();
        ApplyingMaskComplete();
    }




    private void UpdateMaskRenderer()
    {
        
        Color color = MaskMakingLevel.Instance.bowlController.colorOfPaste;

        foreach (var renderer in masksMesheRenderers)
        {
            foreach (var material in renderer.materials)
            {
                material.color = color;
                material.SetColor("_EmissionColor", color);
            }
        }

    }

    private void MoveToTargetPosition()
    {
        if (!maskApplyingOnce)
            return;
        if (mask.GetComponent<MaskReachTarget>().maskReachedTarget)
            return;

        float Mouse_Y = Mathf.Clamp(Input.GetAxis("Mouse Y"), 0,Mathf.Infinity);
       
        if (Mouse_Y >= 0.05f && Input.GetMouseButton(0))
        {
           
            mask.SetFloat("Speed", 1);
           
        }
    }
    private void ReachedTargetOnce()
    {
        if (!mask.GetComponent<MaskReachTarget>().maskReachedTarget)
            return;

        if (!maskReachedPositionOnce)
        {
            MaskToTarget.SetActive(false);
            maskOnFace.SetActive(true);

            maskReachedPositionOnce = true;
        }
    }

    private void ApplyingMaskComplete()
    {
        if (!mask.GetComponent<MaskReachTarget>().maskReachedTarget)
            return;
        if (!maskReachedPositionOnce)
            return;
        if (!maskOnFace.GetComponent<FaceMask>().faceTakOffCheck)
            return;



        if(!processComplete)
        {
            StartCoroutine(TakingOfSuccess());
           
            processComplete = true;
        }

      

    }
    IEnumerator TakingOfSuccess()
    {
      
        yield return new WaitForSeconds(1.05f);
        maskOnFace.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Character.Play("Happy01");
        //WinSource.Play();
        yield return new WaitForSeconds(2f);

        FinishUIPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        GamePlayScene Controller = FindAnyObjectByType<GamePlayScene>();
        Controller.SkipBtn.SetActive(true);
    }








}
