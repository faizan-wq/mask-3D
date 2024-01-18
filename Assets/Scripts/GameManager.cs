using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Show UI")]
    public GameObject GameUI;
    public GameObject InRoomUI;

    [Header("Panels Controller")]
    public GameObject PanelSettings;
    public GameObject PanelPause;

    void Start()
    {
        CheckingDone();
    }
    public void SkipeBtnWin()
    {
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 379);
        PlayerPrefs.SetString("Mode", "");
        PlayerPrefs.SetString("Scene", "");
        SceneManager.LoadScene(0);
    }
    public void RewaredBtnWin()
    {
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 1137);
        PlayerPrefs.SetString("Mode", "");
        PlayerPrefs.SetString("Scene", "");
        SceneManager.LoadScene(0);
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
