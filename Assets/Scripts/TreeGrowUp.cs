using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TreeGrowUp : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource HiteSource;
    public AudioSource SearchSource;

    [Header("Animators Controller")]
    public Animator BigTree;
    public Animator ShortGrass;
    public Animator PlasticBox;

    [Header("Controller Appel")]
    public GameObject[] ListAppels;

    [Header("Container Controller")]
    public GameObject ContainerCash;
    public GameObject Cash;

    [Header("List")]
    public GameObject TakeOne;
    public GameObject TakeTwo;
    public GameObject TakeThree;
    public GameObject ContainerFillingBar;
    public GameObject FillingBar;

    private Vector3 offset;
    private bool isDragging = false;
    public float JumpSpeed = 1000;

    [Header("Floating")]
    internal float MovementController = 0;
    internal float TimeDrop = 7;

    [Header("Objects")]
    public GameObject TutorialWater;
    void Awake()
    {
        TakeOne.SetActive(true);
        TakeThree.SetActive(true);
        TakeTwo.SetActive(true);
        ContainerFillingBar.SetActive(true);
        FillingBar.SetActive(true);
        StartCoroutine(StartAnimator());
        Detecteur.gameObject.SetActive(true);
    }
    public void OnMouseDown()
    {
        if (StartMoving)
        {
            TutorialWater.SetActive(false);
            offset = WaterCan.transform.localPosition - GetMouseWorldPosition();
            BigTree.enabled = true;
            BigTree.StartPlayback();
            BigTree.speed = 1;
            WaterCan.GetComponent<Animator>().Play("PourWaterAnim");
            WaterEffect.Play();
            WaterEffect.gameObject.GetComponent<AudioSource>().Play();
            isDragging = true;
        }
        else if(SHootAgain)
        {
            HandTutorial.SetActive(false);
            HandObj.GetComponent<Animator>().Play("TakeFruit");
            HiteSource.Play();
            StartCoroutine(LoadingHandActivate());
            SHootAgain = false;
        }
    }

    public void OnMouseUp()
    {
        if (StartMoving)
        {
            BigTree.StopPlayback();
            BigTree.speed = 0;
            WaterCan.GetComponent<Animator>().Play("PourWaterAnimReverse");
            WaterEffect.Stop();
            WaterEffect.gameObject.GetComponent<AudioSource>().Stop();
            isDragging = false;
        }
        else if(FinishGrowing) { FillingBar.gameObject.GetComponent<Image>().fillAmount = 0f; WaterCan.GetComponent<Animator>().Play("PourWaterAnimReverse"); HandTutorial.SetActive(true); BigTree.StopPlayback(); WaterEffect.Stop(); FinishGrowing = false; DoneLevel.Play(); DoneLevel.gameObject.GetComponent<AudioSource>().Play(); TakeHand = true; } else
        {

        }
    }

    void Update()
    {
        ManagerFirstMove();
        if (TakeHand)
        {
            HandObj.SetActive(true);
            Vector3 PositionMoveTo = new Vector3(0, 6.71f, -29.13f);
            HandObj.transform.localPosition = Vector3.Lerp(HandObj.transform.localPosition, PositionMoveTo, 0.05f);
            if(FillingBar.GetComponent<Image>().fillAmount == 1f)
            {
                Vector3 Mover = new Vector3(-40.87f, 2.74f, -7.24f);
                BasketBox.transform.localPosition = Vector3.Lerp(BasketBox.transform.localPosition, Mover, 0.05f);
                HandObj.SetActive(false);
                if (CheckDone)
                {
                    StartCoroutine(LoadingCameraMovement());
                    CheckDone = false;
                }else if (CheckCameraMoving)
                {
                    if (Verifie)
                    {
                        StartCoroutine(LoadingHandActivate());
                        Verifie = false;
                    }
                    CameraView.transform.position = Vector3.Lerp(CameraView.transform.localPosition, FinishPoint.transform.position, 0.05f);
                    CameraView.transform.eulerAngles = Vector3.Lerp(CameraView.transform.eulerAngles, FinishPoint.transform.eulerAngles, 0.05f);
                    Vector3 VecLice = Vector3.zero;
                    BasketBox.transform.position = Vector3.Lerp(BasketBox.transform.position, PositionBaskest.transform.position, 1.5f);
                }
            }
        }
    }
    void ManagerFirstMove()
    {
        if (Scaler.transform.localScale.x == 4.25f)
        {
            TakeOne.transform.GetChild(0).gameObject.SetActive(true);
            StartMoving = false;
            if (WaterCan.transform.localPosition.x > 0 && FinishGrowing == false)
            {
                if (CheckLoader)
                {
                    StartCoroutine(LoadingFinish());
                    CheckLoader = false;
                }
                MovementController += Time.deltaTime * 5;
                WaterCan.transform.localPosition = new Vector3(WaterCan.transform.localPosition.x + MovementController, WaterCan.transform.localPosition.y, WaterCan.transform.localPosition.z);
            }
            else if (WaterCan.transform.localPosition.x < 0 && FinishGrowing == false)
            {
                if (CheckLoader)
                {
                    StartCoroutine(LoadingFinish());
                    CheckLoader = false;
                }
                MovementController -= Time.deltaTime * 5;
                WaterCan.transform.localPosition = new Vector3(WaterCan.transform.localPosition.x + MovementController, WaterCan.transform.localPosition.y, WaterCan.transform.localPosition.z);
            }
        }
        else
        {
            if (isDragging && StartMoving && FinishGrowing == true)
            {
                FillingBar.gameObject.GetComponent<Image>().fillAmount = (float)(Scaler.transform.localScale.x / 4.25f);
                Vector3 newPosition = GetMouseWorldPosition() + offset;
                float MaxMovement = Mathf.Clamp(newPosition.x, -3f, 3f);
                float MaxMovementY = Mathf.Clamp(newPosition.y, -5.9f, 5.9f);
                WaterCan.transform.localPosition = new Vector3(MaxMovement, MaxMovementY, 0);
                if (WaterCan.transform.localPosition.x > 0 && CheckActiveDirection)
                {
                    WaterCan.GetComponent<Animator>().Play("MoveXAnim 0");
                    CheckActiveDirection = false;
                }
                else if (WaterCan.transform.localPosition.x < 0 && CheckActiveDirection == false)
                {
                    WaterCan.GetComponent<Animator>().Play("MoveXAnim");
                    CheckActiveDirection = true;
                }
            }
            else
            {
                BigTree.StopPlayback();
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    /// <summary>
    /// /////
    /// </summary>
    IEnumerator StartAnimator()
    {
        yield return new WaitForEndOfFrame();
        BigTree.enabled = false;
    }
    IEnumerator LoadingFinish()
    {
        yield return new WaitForSeconds(0.5f);
        WaterCan.gameObject.SetActive(false);
    }
    IEnumerator LoadingCameraMovement()
    {
        yield return new WaitForSeconds(2f);
        CheckCameraMoving = true;
    }
    IEnumerator LoadingHandActivate()
    {
        yield return new WaitForSeconds(0.25f);
        BigTree.Play("Vibration");
        HitedShoot.Play();
        int RandomSelection = Random.Range(0, 2);
        if(RandomSelection == 1 && TimeDrop > 0 && FillingBar.GetComponent<Image>().fillAmount != 1)
        {
            int RandomAppel = Random.Range(0, ListAppels.Length);
            Debug.Log("RandomAppel:"+ RandomAppel);
            if(RandomAppel < ListAppels.Length - 1)
            {
                (Instantiate(Cash, Cash.transform.position, Quaternion.identity) as GameObject).transform.SetParent(ContainerCash.transform);
                FillingBar.GetComponent<Image>().fillAmount += 0.1428571428571429f;
                ListAppels[RandomAppel].transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                ListAppels[RandomAppel].transform.GetChild(0).gameObject.GetComponent<Appel>().IsAppel = true;
                ListAppels[RandomAppel].transform.GetChild(0).transform.parent = null;
                ArrayUtility.Remove(ref ListAppels, ListAppels[RandomAppel]);
                
                TimeDrop -= 1;
            }
        }
        else if(FillingBar.GetComponent<Image>().fillAmount == 1)
        {
            DoneLevel.Play();
            DoneLevel.gameObject.GetComponent<AudioSource>().Play();
            TakeTwo.transform.GetChild(0).gameObject.SetActive(true);
            Appel[] ListAppels = FindObjectsOfType<Appel>();
            foreach(Appel appel in ListAppels)
            {
                if (appel.IsAppel)
                {
                    appel.StartJump = true;
                }
            }
            yield return new WaitForSeconds(1.4f);
            BoxColliderHidden.SetActive(false);
            BasketBox.GetComponent<Animator>().Play("Controller");
            yield return new WaitForSeconds(3f);
            PlasticBox.Play("CloseBox");
            SearchSource.Play();
            yield return new WaitForSeconds(2f);
            FinishUI.SetActive(true);
            AppelFinish.SetActive(true);
        }
        yield return new WaitForSeconds(0.25f);
        SHootAgain = true;
    }
    [Header("UI Finish")]
    public GameObject FinishUI;
    public GameObject AppelFinish;

    [Header("UI COntroller")]
    public GameObject BoxColliderHidden;

    [Header("Camera")]
    public GameObject CameraView;
    public GameObject FinishPoint;

    [Header("Object Position")]
    public GameObject PositionBaskest;

    [Header("UI Controller Manager")]
    public GameObject HandObj;
    public GameObject HandTutorial;

    [Header("Particle System")]
    public ParticleSystem WaterEffect;
    public ParticleSystem DoneLevel;
    public ParticleSystem HitedShoot;

    [Header("Component Controller")]
    public GameObject WaterCan;
    public GameObject Detecteur;
    public GameObject Scaler;
    public GameObject BasketBox;

    [Header("Boolean Manager")]
    internal bool CheckActiveDirection = true;
    internal bool StartMoving = true;
    internal bool FinishGrowing = true;
    internal bool TakeHand = false;
    internal bool SHootAgain = true;
    internal bool CheckDone = true;
    internal bool CheckCameraMoving = false;
    internal bool CheckLoader = true;
    internal bool Verifie = true;
}
