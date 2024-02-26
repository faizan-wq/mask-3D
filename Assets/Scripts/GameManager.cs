using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GD;
public class GameManager : MonoBehaviour
{
    [Header("Show UI")]
    public GameObject GameUI;
    public GameObject InRoomUI;

    [Header("Panels Controller")]
    public GameObject PanelSettings;
    public GameObject PanelPause;


    public Transform diamondTarget;

    void Start()
    {
        CheckingDone();
    }
    public void SkipeBtnWin()
    {

        FlyingDiamond cashTemp = DailyRewardManager.Instance.flyingDiamondPrefab;
        StartCoroutine(ApplyFunctionAfterWait(delegate {
            cashTemp.MoveToTarget(diamondTarget, 150,null );
            Invoke(nameof(GameComplete), 2);
           
        },1));
      

    }
    private void GameComplete()
    {
        PlayerPrefs.SetString("Mode", "");
        PlayerPrefs.SetString("Scene", "");
        SceneManager.LoadScene("Start");
        PlayerPrefs.SetInt("Days", PlayerPrefs.GetInt("Days") + 1);
    }



    public void RewaredBtnWin()
    {
        FlyingDiamond cashTemp = DailyRewardManager.Instance.flyingDiamondPrefab;
        
        AdMob_GF.ShowRewardedAdmobOrInterstitial();
        Invoke(nameof(GameComplete), 2);
    }  

    IEnumerator  ApplyFunctionAfterWait(Action action, float value)
    {
        yield return new WaitForSeconds(value);
        action?.Invoke();
    }


    /// <summary>
    /// ///
    /// </summary>
    public void SetHomeBack()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetString("Scene", "");
        SceneManager.LoadScene(0);
    }
    public void OpenSetting()
    {
        BoolSettings = !BoolSettings;
        if (BoolSettings)
        {
            PanelSettings.SetActive(true);
        }
        else if(BoolSettings == false)
        {
            PanelSettings.GetComponent<Animator>().Play("Hide");
            StartCoroutine(LoadingClose());
        }
    }
    public void SetPause()
    {
        BoolPause = !BoolPause;
        if (BoolPause)
        {
            StartCoroutine(PauseLoading());
            PanelPause.SetActive(true);
        }
        else if(BoolPause == false)
        {
            Time.timeScale = 1.0f;
            PanelPause.GetComponent<Animator>().Play("Hide");
            StartCoroutine(LoadingClosePause());
        }
    }
    /// <summary>
    /// ////////
    /// </summary>
    void CheckingDone()
    {
        if(PlayerPrefs.GetString("Scene") == "0")
        {
            GameUI.SetActive(true);
        }
        else if(PlayerPrefs.GetString("Scene") == "1")
        {
            InRoomUI.SetActive(true);
        }
    }
    /// <summary>
    /// //////
    /// </summary>
    IEnumerator PauseLoading()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.0f;
    }
    IEnumerator LoadingClosePause()
    {
        yield return new WaitForSeconds(0.5f);
        PanelPause.SetActive(false);
    }
    IEnumerator LoadingClose()
    {
        yield return new WaitForSeconds(0.5f);
        PanelSettings.SetActive(false);
    }
    [Header("Boolean Manager")]
    internal bool BoolSettings = false;
    internal bool BoolPause = false;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


}
