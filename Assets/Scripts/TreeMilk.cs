using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TreeMilk : MonoBehaviour
{
    [Header("Boolean Manager")]
    internal bool CheckDown = true;

    [Header("Particle System")]
    public ParticleSystem EffectDoneTask;

    [Header("Floating Controller")]
    internal float RotationMoving = 0;

    [Header("Audio Source")]
    public AudioSource ClickedPlace;
    public AudioSource TimeFinish;
    public AudioSource WinSource;

    [Header("TakingList")]
    public GameObject TakeOne;
    public GameObject TakeTwo;
    void Start()
    {
        foreach(GameObject obj in ListEffects)
        {
            TakeOne.SetActive(true);
            TakeTwo.SetActive(true);
            if(obj.name == PlayerPrefs.GetString("Mode"))
            {
                obj.SetActive(true);
            }
        }
        ButtonMachine.Play("Shake");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hits a 3D object
                if (hit.collider != null)
                {
                    ClickedPlace.Play();
                    // Perform actions on the clicked object
                    GameObject clickedObject = hit.collider.gameObject;
                    if(clickedObject.name == "MoveToTargetTask" && CheckDown == true)
                    {
                        if(ActivateMachine == false)
                        {
                            ButtonMachine.Play("ButtonPressed");StartCoroutine(StartLoadingFace());
                            ActivateMachine = true;
                        }else if(ActivateMachine == true&& NextTakeMask == true)
                        {
                            TutorialFaceMoving.SetActive(true);
                            StartCoroutine(LoadingNext());
                            TakeMask.gameObject.SetActive(true);
                            MaskFace.gameObject.SetActive(false);
                            TutorialCanvas.SetActive(false);
                            TakeMask.Play("Take001");
                            NextTakeMask = false;
                        }
                        CheckDown = false;
                    }
                    if(NextTakeMask == false)
                    {
                        if (FixedFace)
                        {
                            TutorialFaceMoving.SetActive(false);
                            FaceStatic.GetComponent<Animator>().enabled = true;
                            StartCoroutine(CourotineFace());
                            FixedFace = false;
                        }
                    }else if(StartCourotine == true)
                    {
                        MoveDownUI = true;
                    }
                }
            }
        }
        else { CheckDown = true; }
        if (StartCourotine == true && FixedFace == false && MoveDownUI == false)
            {
                if (Fillingbar.fillAmount != 0)
                {
                    RotationMoving = 26 * Time.deltaTime;
                    ArrowRotating.transform.Rotate(0, 0, RotationMoving);
                    Fillingbar.fillAmount -= Time.unscaledDeltaTime / 15;
                }
                else
                {
                EffectDoneTask.Play();
                EffectDoneTask.gameObject.GetComponent<AudioSource>().Play();
                TakeTwo.transform.GetChild(0).gameObject.SetActive(true);
                ClockView.SetActive(false);
                TimeFinish.Play();
                foreach (GameObject obj in ListEffects)
                {
                    if (obj.name == PlayerPrefs.GetString("Mode"))
                    {
                        obj.SetActive(false);
                    }

                }
                StartCoroutine(TakingOf());
                FaceMoving.GetComponent<Animator>().Play("Take001");
                FixedFace = true;
                }
            }
        if (MoveCameraToTarget)
        {
            CameraObj.transform.eulerAngles = Vector3.Lerp(CameraObj.transform.eulerAngles, CameraPos.transform.eulerAngles, 0.05f);
            CameraObj.transform.position = Vector3.Lerp(CameraObj.transform.position, CameraPos.transform.position, 0.05f);
        }
    }
    /// <summary>
    /// //////
    /// </summary>
    IEnumerator StartLoadingFace()
    {
        yield return new WaitForSeconds(1f);
        EffectDoneTask.Play();
        EffectDoneTask.gameObject.GetComponent<AudioSource>().Play();
        TakeOne.transform.GetChild(0).gameObject.SetActive(true);
        MaskFace.Play("Liquide");
        MaskFace.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.6f);
        TutorialCanvas.SetActive(true);
        NextTakeMask = true;
    }
    IEnumerator LoadingNext()
    {
        yield return new WaitForSeconds(1.5f);
        MaskFace.gameObject.SetActive(false);
        MoveCameraToTarget = true;
        GirlAnimator.Play("Idle 0");
        FaceStatic.SetActive(true);
        NextTakeMask = false;
        FixedFace = true;
    }
    IEnumerator CourotineFace()
    {
        yield return new WaitForSeconds(0.2f);
        StartCourotine = true;
        FaceOutline.SetActive(false);
        FaceMoving.SetActive(true);
        FaceMoving.GetComponent<Animator>().Play("Show");
        FaceStatic.SetActive(false);
        ClockView.SetActive(true);
    }
    IEnumerator TakingOf()
    {
        yield return new WaitForSeconds(1.05f);
        FaceMoving.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        GirlAnimator.Play("Happy01");
        WinSource.Play();
        yield return new WaitForSeconds(2f);
        TakeOne.SetActive(false);
        TakeTwo.SetActive(false);
        FinishUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        GamePlayScene Controller = FindAnyObjectByType<GamePlayScene>();
        Controller.SkipBtn.SetActive(true);
    }
    [Header("CameraTransformer")]
    public GameObject CameraObj;
    public GameObject CameraPos;
    

    [Header("UI Controller")]
    public GameObject ClockView;
    public GameObject ArrowRotating;
    public Image Fillingbar;

    [Header("Finish UI")]
    public GameObject FinishUI;

    [Header("List Face Effects")]
    public GameObject[] ListEffects;
    public GameObject FaceMoving;
    public GameObject FaceStatic;
    public GameObject FaceOutline;

    [Header("Animation")]
    public Animator ButtonMachine;
    public Animator MaskFace;
    public Animator TakeMask;
    public Animator GirlAnimator;

    [Header("GameLogic")]
    internal bool ActivateMachine = false;
    internal bool NextTakeMask = false;
    internal bool MoveCameraToTarget = false;
    internal bool FixedFace = false;
    internal bool StartCourotine = false;
    internal bool MoveDownUI = false;

    [Header("Tutorials")]
    public GameObject TutorialCanvas;
    public GameObject TutorialFaceMoving;
}
