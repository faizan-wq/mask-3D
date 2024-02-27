using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatorMilkUI : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource HiteSound;
    public AudioSource Complete;
    public AudioSource CowSound;
    public AudioSource WaterPouling;

    [Header("Controller Objects")]
    public GameObject WeaponOne;
    public GameObject WeaponTwo;
    public GameObject WeaponThree;

    [Header("Controller Shower")]
    public GameObject ShowerFinish;

    [Header("Animator")]
    public Animator Cow;
    public Animator CowVibration;
    public Animator MilkBarel;
    public Animator MachineGenerator;
    public Animator BoxAnimated;

    [Header("Particls")]
    public ParticleSystem TakeEffect;
    public ParticleSystem MilkFlow;
    public ParticleSystem WaterDrop;
    public ParticleSystem ThrowUpWater;

    [Header("Texture")]
    public GameObject TextureCamera;

    [SerializeField]
    private GameObject CurrentActive;
    void Start()
    {
        TaskOne.SetActive(true);
        TaskTwo.SetActive(true);
        TaskThree.SetActive(true);
        TaskFour.SetActive(true);
        FillingBar.fillAmount = 0f;
    }

    private bool setTwoAd = false;
    private bool setThreeAd = false;


    public void SetOne()
    {


        ManagerSpawning.ContainerUI.SetActive(false);
        ManagerSpawning.CreatorMilkUI.SetActive(false);
        TakeEffect.Play();
        Complete.Play();
        WeaponOne.SetActive(true);
        ClickedUI.gameObject.SetActive(true);
        TaskOne.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void SetTwo()
    {

        GD.Controller.Instance.RewardedVideo(result => {

            if (result)
            {

                ManagerSpawning.ContainerUI.SetActive(false);
                ManagerSpawning.CreatorMilkUI.SetActive(false);
                TakeEffect.Play();
                Complete.Play();
                WeaponTwo.SetActive(true);
                ClickedUI.gameObject.SetActive(true);
                TaskOne.transform.GetChild(0).gameObject.SetActive(true);
            }


        });

       
    }
    public void SetThree()
    {

        GD.Controller.Instance.RewardedVideo(result => {

            if (result)
            {
                ManagerSpawning.ContainerUI.SetActive(false);
                ManagerSpawning.CreatorMilkUI.SetActive(false);
                TakeEffect.Play();
                Complete.Play();
                WeaponThree.SetActive(true);
                ClickedUI.gameObject.SetActive(true);
                TaskOne.transform.GetChild(0).gameObject.SetActive(true);
            }


        });


       
    }
    public void SetActive(bool CheckDone)
    {
        CheckClicked = CheckDone;
    }
    public void SetPositionFloating(bool IsActive)
    {
        DraggingAnimation = IsActive;
        if (IsActive)
        {
            HideUIWheenClick = true;
            SpawnCoolder = true;
            offset = MilkBarel.transform.position - GetMouseWorldPosition();
            TutorialUIContainer.SetActive(false);
            StartMovingBarrel = IsActive;
        }
        else
        {
            StartMovingBarrel = IsActive;
        }
    }
    public void StartDragings(bool IsDeagging)
    {
        if (StartDraging)
        {
            TutorialUIContainer.SetActive(false);
        }
        else { }
    }
    /// <summary>
    /// /
    /// </summary>
    void Update()
    {
        CheckingMilk();
        if (FinishItCollecting)
        {
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, CameraPos.transform.position, 0.2f);
            Barrel.transform.position = Vector3.Lerp(Barrel.transform.position, BarrelPos.transform.position, 0.1f);
            float DistanceBtw = Vector3.Distance(MainCamera.transform.position, CameraPos.transform.position);
            if (DistanceBtw < 0.1f)
            {
                TaskTwo.transform.GetChild(0).gameObject.SetActive(true);
                FinishItCollecting = false;
            }
        } else if (FinishItCollecting == false && FinishDrooping)
        {
            if (StartMovingBarrel)
            {
                if (MovingFilling != 0 && FinishDrooping)
                {
                    ContainerBar.gameObject.SetActive(true);
                    FillingBar.gameObject.SetActive(true);
                    FillingBar.fillAmount = MovingFilling;
                    if (FillingBar.fillAmount == 1)
                    {
                        FillingBar.gameObject.SetActive(false);
                        ContainerBar.gameObject.SetActive(false);

                        TaskThree.transform.GetChild(0).gameObject.SetActive(true);
                        FinishDrooping = false;
                    }
                }
                if (DraggingAnimation == true)
                {
                    TutorialUIContainer.SetActive(false);
                    Vector3 targetPosition = GetMouseWorldPosition() + offset;
                    Vector3 Transformer = new Vector3(targetPosition.x, MilkBarel.transform.position.y, MilkBarel.transform.position.z);
                    float LevelX = Mathf.Clamp(Transformer.x, MinXMove, MaxXMove);
                    MilkBarel.transform.position = new Vector3(LevelX, MilkBarel.transform.position.y, MilkBarel.transform.position.z);
                    if (CheckCourotine)
                    {
                        StartCoroutine(DropingWater());
                        MilkBarel.Play("PourWaterAnim");
                        CheckCourotine = false;
                    }
                }
            }
            else if (!StartMovingBarrel)
            {
                if (CheckCourotine == false)
                {
                    WaterDrop.Stop();
                    WaterPouling.Stop();
                    MilkBarel.Play("PourWaterAnimReverse");
                    CheckCourotine = true;
                }
            }
        } else if (FinishDrooping == false)
        {
            if (CheckCloseFinish == false)
            {
                Barrel.gameObject.SetActive(false);
                MainCamera.gameObject.transform.position = Vector3.Lerp(MainCamera.transform.position, CameraSecondPos.transform.position, 0.1f);
                MainCamera.transform.eulerAngles = Vector3.Lerp(MainCamera.transform.eulerAngles, CameraSecondPos.transform.eulerAngles, 0.1f);
                float PosLenght = Vector3.Distance(MainCamera.transform.position, CameraSecondPos.transform.position);
                if (PosLenght < 0.1f)
                {
                    FillingBar.fillAmount = 0f;
                    TakeEffect.Play();
                    Complete.Play();
                    CheckCloseFinish = true;
                }
            }
            else if (CheckCloseFinish == true)
            {
                if (HideUIWheenClick)
                {
                    UIFinishStep.gameObject.SetActive(false);
                } else { UIFinishStep.gameObject.SetActive(true); }
                if (HideUIWheenClick == true && SpawnCoolder == true)
                {
                    MachineGenerator.Play("CreateMilkMachine");
                    int RandomSelection = Random.Range(0, ListMilks.Length);
                    ListMilks[RandomSelection].gameObject.SetActive(true);
                    (Instantiate(PrefabeUICoin, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.SetParent(ContainerUICoin.transform);

                    FlyingDiamond cashTemp = DailyRewardManager.Instance.flyingDiamondPrefab;

                    cashTemp.MoveToTarget(diamondTarget, 20);

                    SpawnCoolder = false;
                }
                else if (FillingBar.fillAmount < 1)
                {
                    ContainerBar.gameObject.SetActive(true);
                    FillingBar.gameObject.SetActive(true);
                    FillingBar.fillAmount = MovingUI;
                } else if (FillingBar.fillAmount >= 1)
                {
                    ClickedUI.gameObject.SetActive(false);
                    ContainerBar.gameObject.SetActive(false);
                    FillingBar.gameObject.SetActive(false);
                    if (CheckCollected)
                    {
                        StartCoroutine(LoadingFinish());
                        CheckCollected = false;
                    }
                }
            }
        }
    }
    void CheckingMilk()
    {
        if (WeaponOne.activeSelf)
            CurrentActive = WeaponOne;
        if (WeaponTwo.activeSelf)
            CurrentActive = WeaponTwo;
        if (WeaponThree.activeSelf)
            CurrentActive = WeaponThree;
        if (ClickedUI.gameObject.activeSelf == true && CurrentActive.GetComponent<Animator>() != null && CurrentActive.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            // The animator is still playing an animation           
        }
        else if (MovingFilling == 0)
        {
            if (WeaponOne.activeSelf == true && CheckClicked == true && ClickedUI.gameObject.activeSelf == true && FillingBar.fillAmount != 1 && StopClicking == false)
            {
                ContainerBar.gameObject.SetActive(true);
                FillingBar.gameObject.SetActive(true);
                FillingBar.fillAmount += 0.1666666666666667f;
                HiteSound.Play();
                CowSound.Play();
                WeaponOne.GetComponent<Animator>().Play("Attack");
                StartCoroutine(LoadingCow());
                CheckClicked = false;
            }
            else if (CheckClicked) { }
            if (WeaponTwo.activeSelf == true && CheckClicked == true && ClickedUI.gameObject.activeSelf == true && FillingBar.fillAmount != 1 && StopClicking == false)
            {
                ContainerBar.gameObject.SetActive(true);
                FillingBar.gameObject.SetActive(true);
                FillingBar.fillAmount += 0.1666666666666667f;
                CowSound.Play();
                WeaponTwo.GetComponent<Animator>().Play("Attack");
                HiteSound.Play();
                StartCoroutine(LoadingCow());
                CheckClicked = false;
            }
            else if (CheckClicked) { }
            if (WeaponThree.activeSelf == true && CheckClicked == true && ClickedUI.gameObject.activeSelf == true && FillingBar.fillAmount != 1 && StopClicking == false)
            {
                ContainerBar.gameObject.SetActive(true);
                FillingBar.gameObject.SetActive(true);
                FillingBar.fillAmount += 0.1666666666666667f;
                CowSound.Play();
                WeaponThree.GetComponent<Animator>().Play("Attack");
                HiteSound.Play();
                StartCoroutine(LoadingCow());
                CheckClicked = false;
            }
            else if (CheckClicked) { }
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
    IEnumerator DropingWater()
    {
        yield return new WaitForSeconds(0.8f);
        WaterDrop.Play();
        WaterPouling.Play();
    }
    IEnumerator StateFinish()
    {
        yield return new WaitForSeconds(3f);
        GamePlayScene Manager = FindAnyObjectByType<GamePlayScene>();
        ShowerFinish.SetActive(true);
        Manager.FinishUI.SetActive(true);
        TextureCamera.SetActive(true);
    }
    IEnumerator LoadingFinish()
    {
        TaskFour.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        BoxAnimated.Play("CloseCartonBox");
        StartCoroutine(StateFinish());
    }
    IEnumerator LoadingCow()
    {
        yield return new WaitForSeconds(0.3f);
        CowVibration.Play("Vibration");
        Cow.Play("MilkProcess");
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(FloatingMilk());
        MilkFlow.Play();
        WaterPouling.Play();
    }
    IEnumerator FloatingMilk()
    {
        yield return new WaitForSeconds(0.6f);
        MilkFlow.Stop();
        WaterPouling.Stop();
        yield return new WaitForSeconds(0.8f);
        if (FillingBar.fillAmount > 0.9f)
        {
            //TaskTwo.transform.GetChild(0).gameObject.SetActive(true);
            MilkBarel.Play("FullMilkAnim");
            Destroy(ClickedUI.gameObject.GetComponent<EventTrigger>());
            StopClicking = true;
        }
        yield return new WaitForSeconds(0.4f);
        if (FillingBar.fillAmount == 1)
        {
            //TaskThree.transform.GetChild(0).gameObject.SetActive(true);
            ContainerBar.gameObject.SetActive(false);
            FillingBar.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            TutorialUIContainer.gameObject.SetActive(true);
            ClickedUI.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            TakeEffect.Play();
            Complete.Play();
            FinishItCollecting = true;
        }
    }
    [Header("List Milks")]
    public GameObject[] ListMilks;

    [Header("Transformer")]
    internal Vector3 offset;

    [Header("Controller Manager")]
    public LevelSpawner ManagerSpawning;

    [Header("Bar Filling")]
    public Image FillingBar;
    public Image ContainerBar;
    public Image ClickedUI;

    [Header("Boolean Manager")]
    internal bool CheckClicked = false;
    internal bool StopClicking = false;
    internal bool FinishItCollecting = false;
    internal bool StartMovingBarrel = false;
    internal bool StartDraging = false;
    internal bool DraggingAnimation = false;
    internal bool CheckCourotine = true;
    internal bool FinishDrooping = true;
    internal bool CheckCloseFinish = false;
    internal bool HideUIWheenClick = false;
    internal bool SpawnCoolder = false;
    internal bool CheckCollected = true;

    [Header("Tasks Complete")]
    public GameObject TaskOne;
    public GameObject TaskTwo;
    public GameObject TaskThree;
    public GameObject TaskFour;

    [Header("Object UI")]
    public GameObject PrefabeUICoin;
    public GameObject ContainerUICoin;

    [Header("Next Object")]
    public GameObject MainCamera;
    public GameObject Barrel;
    public GameObject TutorialUIContainer;
    public GameObject UIFinishStep;

    [Header("Position Controller")]
    public Transform CameraPos;
    public Transform BarrelPos;
    public Transform CameraSecondPos;

    [Header("Floating Controller")]
    public float MinXMove = -50f;
    public float MaxXMove = 50f;
    public float MovingFilling = 0;
    public float MovingUI;

    [Header("Diamond")]
    public Transform diamondTarget;

}
