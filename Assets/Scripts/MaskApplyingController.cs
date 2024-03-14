using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskApplyingController : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> masksMesheRenderers;
    [SerializeField] private GameObject MaskToTarget;
    [SerializeField] private GameObject FinishUIPanel;

    [SerializeField] private Animator mask;
    [SerializeField] private Animator Character;

    [SerializeField] private GameObject maskOnFace;
    public GameObject Tutorial;
    public GameObject PoisonParticle;
    public GameObject poisonface;
    public GameObject vomitParticle;
    private bool maskReachedPositionOnce = false;
    private bool processComplete;
    private Transform Characterparent;
    private bool maskApplyingOnce;

    private SoundManager soundManager;


    private void Start()
    {
        soundManager = ParticleManager.Instance.soundManager;
    }


    public void ApplyOnStart()
    {
        if (!maskApplyingOnce)
        {
            UpdateMaskRenderer();
            mask.gameObject.SetActive(true);
            EnablePoisonParticle();
            Tutorial.SetActive(true);
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


    private void EnablePoisonParticle()
    {
        if (MaskMakingLevel.Instance.bottleController.selectedBottle.prefab.name == "Chemical X")
        {
            PoisonParticle.SetActive(true);
           
            PoisonParticle.GetComponent<PlayParticleAAfterWait>().deathEffectStart = true;
        }
    }
    private void Enablefacemask()
    {
        if (MaskMakingLevel.Instance.bottleController.selectedBottle.prefab.name == "Chemical X")
        {
            poisonface.SetActive(true);

            poisonface.GetComponent<PlayParticleAAfterWait>().deathEffectStart = true;

            Characterparent = Character.transform.parent;

        }
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

            Tutorial.SetActive(false);

        }
    }
    private void ReachedTargetOnce()
    {
        if (!mask.GetComponent<MaskReachTarget>().maskReachedTarget)
            return;

        if (!maskReachedPositionOnce)
        {
            MaskToTarget.SetActive(false);
            Enablefacemask();

            maskOnFace.SetActive(true);

            if (ItemsManager.Instance.selectedItem.itemType != Item_Type.Good || MaskMakingLevel.Instance.bottleController.selectedBottle.itemType != Item_Type.Good)
            {
                
                maskOnFace.GetComponent<Animator>().SetFloat("Speed", 0f);

            }
           
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

            if (ItemsManager.Instance.selectedItem.itemType == Item_Type.Good && MaskMakingLevel.Instance.bottleController.selectedBottle.itemType == Item_Type.Good)
            {
                StartCoroutine(TakingOfSuccess());

            }
            else if (MaskMakingLevel.Instance.bottleController.selectedBottle.prefab.name == "Chemical X")
            {
                StartCoroutine(FailureVomit());

            }
            else
            {
                StartCoroutine(FailureSad());
            }
            processComplete = true;
        }

      

    }
    IEnumerator TakingOfSuccess()
    {
        MaskMakingLevel.Instance.EnableTaskPoint(6, 1);
        Transform maskParent = maskOnFace.transform.parent;
        for (int i = 0; i < maskParent.childCount; i++)
        {
            if (maskParent.GetChild(i).gameObject!=maskOnFace)
            {
                maskParent.GetChild(i).gameObject.SetActive(false);
            }
          
        }
       
        yield return new WaitForSeconds(3f);
        maskOnFace.SetActive(false);
      
        yield return new WaitForSeconds(0.3f);
        Character.Play("Happy01");
        soundManager.PlayQuickSoundClip("Girl happy");
        //WinSource.Play();
        yield return new WaitForSeconds(2f);
        MaskMakingLevel.Instance.camera.transform.GetChild(1).gameObject.SetActive(true);
        FinishUIPanel.SetActive(true);
        soundManager.PlayQuickSoundClip("camera Sound");
        yield return new WaitForSeconds(2f);
        GamePlayScene Controller = FindAnyObjectByType<GamePlayScene>();
        Controller.SkipBtn.SetActive(true);
    }





    IEnumerator FailureVomit()
    {
        MaskMakingLevel.Instance.EnableTaskPoint(6, 1);
        Transform maskParent = maskOnFace.transform.parent;
        for (int i = 0; i < maskParent.childCount; i++)
        {
            if (maskParent.GetChild(i).gameObject != maskOnFace)
            {
                maskParent.GetChild(i).gameObject.SetActive(false);
            }

        }



        
        yield return new WaitForSeconds(0.3f);
        Character.Play("FemaleStanding01");
        vomitParticle.SetActive(true);
        soundManager.PlayQuickSoundClip("Vomit Sound Effect");
        Characterparent.DOLocalRotate(Vector3.up * 360, 1f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);


        //WinSource.Play();
        yield return new WaitForSeconds(2f);
        MaskMakingLevel.Instance.camera.transform.GetChild(1).gameObject.SetActive(true);
        FinishUIPanel.SetActive(true);
        soundManager.PlayQuickSoundClip("camera Sound");
        yield return new WaitForSeconds(2f);
        GamePlayScene Controller = FindAnyObjectByType<GamePlayScene>();
        Controller.SkipBtn.SetActive(true);
    }

    IEnumerator FailureSad()
    {
        MaskMakingLevel.Instance.EnableTaskPoint(6, 1);
        Transform maskParent = maskOnFace.transform.parent;
        for (int i = 0; i < maskParent.childCount; i++)
        {
            if (maskParent.GetChild(i).gameObject != maskOnFace)
            {
                maskParent.GetChild(i).gameObject.SetActive(false);
            }

        }




        yield return new WaitForSeconds(0.3f);
        Character.Play("Sad01");
        soundManager.PlayQuickSoundClip("Cry Sound");

        //WinSource.Play();
        yield return new WaitForSeconds(2f);
        MaskMakingLevel.Instance.camera.transform.GetChild(1).gameObject.SetActive(true);
        FinishUIPanel.SetActive(true);
        soundManager.PlayQuickSoundClip("camera Sound");
        yield return new WaitForSeconds(2f);
        GamePlayScene Controller = FindAnyObjectByType<GamePlayScene>();
        Controller.SkipBtn.SetActive(true);
    }






}
