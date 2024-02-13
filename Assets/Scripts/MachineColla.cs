using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineColla : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource Purchased;
    public AudioSource BrokenMachine;
    public AudioSource CrashAttack;

    [Header("Obj")]
    public GameObject Coins;
    public GameObject MachineCoins;

    [Header("Steps")]
    public GameObject TakeOne;
    public GameObject TakeTwo;
    public GameObject TakeThree;

    [Header("Controller")]
    public GameObject SelectIntegritiy;
    public GameObject Container;

    [Header("Weapons")]
    public GameObject ListWeaponOne;
    public GameObject ListWeaponTwo;

    [Header("Controller Machine")]
    public GameObject MachineSelector;
    public GameObject MachineBox;

    [Header("BarFilling")]
    public GameObject FillingContainer;
    public GameObject FillingBar;

    [Header("List Cola")]
    public GameObject[] ListCoka;

    [Header("Particles")]
    public ParticleSystem EffectDone;

    [Header("UI Finish")]
    public GameObject ContainerUI;
    public GameObject UI;

    [Header("Coins")]
    public GameObject ChckerContainer;
    public GameObject CoinsUI;

    [Header("Camera Changing")]
    public GameObject Camera;
    public GameObject CameraPos;

    [Header("Boolean Manager")]
    internal bool GameFinish = true;
    void Start()
    {
        ChckerContainer.SetActive(true);
        TakeOne.SetActive(true); TakeTwo.SetActive(true); TakeThree.SetActive(true);
    }
    void Update()
    {
        if (FillingBar.gameObject.GetComponent<Image>().fillAmount == 1)
        {
            FillingContainer.SetActive(false);
            FillingBar.SetActive(false);
            TakeThree.transform.GetChild(0).gameObject.SetActive(true);
            if (GameFinish)
            {
                StartCoroutine(FinalizingMachineDistruction());
                GameFinish = false;
            }
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, CameraPos.transform.position, 0.05f);
            Camera.transform.eulerAngles = Vector3.Lerp(Camera.transform.eulerAngles, CameraPos.transform.eulerAngles, 0.05f);
        }
    }

    IEnumerator FinalizingMachineDistruction()
    {

        ListWeaponOne.SetActive(false);
        ListWeaponTwo.SetActive(false);
        yield return new WaitForSeconds(3);
        MachineBox.GetComponent<Animator>().Play("CloseCartonBox");
        EffectDone.Play();
        EffectDone.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(loadingFinish());

    }



    public void SetDown()
    {
        if (FirstStep)
        {
            Coins.GetComponent<Animator>().Play("MoveInside");
            StartCoroutine(LoadingMachine());
            FirstStep = false;
        }
        else if (StartShooting && FillingBar.gameObject.GetComponent<Image>().fillAmount != 1)
        {
            if (allowHittingmachine)
            {

                if (ListWeaponOne.activeSelf)
                {
                    CrashAttack.Play();
                    (Instantiate(CoinsUI, CoinsUI.transform.position, Quaternion.identity) as GameObject).transform.SetParent(ChckerContainer.transform);
                    ListWeaponOne.gameObject.GetComponent<Animator>().Play("Attack");
                    int SetShoot = Random.Range(0, 2);
                    if (SetShoot == 1 && TimeMove > 0)
                    {
                        int randomSelection = Random.Range(0, ListCoka.Length);
                        ListCoka[randomSelection].gameObject.SetActive(true);
                        FillingBar.gameObject.GetComponent<Image>().fillAmount += 0.1f;
                        TimeMove -= 1;
                    }
                }
                if (ListWeaponTwo.activeSelf)
                {
                    CrashAttack.Play();
                    (Instantiate(CoinsUI, CoinsUI.transform.position, Quaternion.identity) as GameObject).transform.SetParent(ChckerContainer.transform);
                    if (FillingBar.gameObject.GetComponent<Image>().fillAmount != 1)
                    {
                        ListWeaponTwo.gameObject.GetComponent<Animator>().Play("Attack");
                        int SetShoot = Random.Range(0, 2);
                        if (SetShoot == 1 && TimeMove > 0)
                        {
                            int randomSelection = Random.Range(0, ListCoka.Length);
                            ListCoka[randomSelection].gameObject.SetActive(true);
                            FillingBar.gameObject.GetComponent<Image>().fillAmount += 0.1f;
                            TimeMove -= 1;
                        }
                    }
                }
                StartCoroutine(allowhittingMachineAfterWait(2));
            }
        }

        else
        {
            ListWeaponOne.SetActive(false);
            ListWeaponTwo.SetActive(false);
        }




    }




    private bool allowHittingmachine=true; 
    IEnumerator allowhittingMachineAfterWait(float wait=0)
    {
        allowHittingmachine = false;
        yield return new WaitForSeconds(wait);
        allowHittingmachine = true;
    }



    public void SetUp()
    {

    }
    public void WeaponOne()
    {
        FillingBar.SetActive(true);
        FillingContainer.SetActive(true);
        TakeTwo.transform.GetChild(0).gameObject.SetActive(true);
        EffectDone.Play();
        EffectDone.gameObject.GetComponent<AudioSource>().Play();
        ////////
        MachineSelector.SetActive(true);
        MachineBox.SetActive(true);
        StartShooting = true;
        ListWeaponOne.SetActive(true);
        SelectIntegritiy.SetActive(false);
        Container.SetActive(false);
    }
    public void WeaponTwo()
    {
        FillingBar.SetActive(true);
        FillingContainer.SetActive(true);
        TakeTwo.transform.GetChild(0).gameObject.SetActive(true);
        EffectDone.Play();
        EffectDone.gameObject.GetComponent<AudioSource>().Play();
        ////////
        MachineSelector.SetActive(true);
        MachineBox.SetActive(true);
        StartShooting = true;
        ListWeaponTwo.SetActive(true);
        SelectIntegritiy.SetActive(false);
        Container.SetActive(false);
    }
    IEnumerator loadingFinish()
    {
        yield return new WaitForSeconds(3f);
        ContainerUI.SetActive(true);
        UI.SetActive(true);
    }
    IEnumerator LoadingMachine()
    {
        yield return new WaitForSeconds(0.7f);
        TakeOne.transform.GetChild(0).gameObject.SetActive(true);
        EffectDone.Play();
        EffectDone.gameObject.GetComponent<AudioSource>().Play();
        Purchased.Play();
        yield return new WaitForSeconds(2f);
        MachineCoins.gameObject.GetComponent<Animator>().Play("BrokenMachine");
        BrokenMachine.Play();
        Coins.SetActive(false);
        yield return new WaitForSeconds(4f);
        BrokenMachine.Stop();
        BrokenMachine.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        SelectIntegritiy.SetActive(true);
        Container.SetActive(true);
    }
    [Header("Boolean Manager")]
    internal bool FirstStep = true;
    internal bool StartShooting = false;

    [Header("Integer Controller")]
    internal int TimeMove = 20;
}
