using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class HomeScene : MonoBehaviour
{
    [Header("Show View")]
    public GameObject ShowSettings;
    public GameObject ShowDaily;

    [Header("UI")]
    public Image IconOne;
    public Image IconTwo;

    [Header("UI")]
    public Text ValueCoins;
    void Update()
    {
        ValueCoins.text = "" + PlayerPrefs.GetInt("Cash");
    }
    public void ISSetting()
    {
        ShowBoolSettings = !ShowBoolSettings;
        if (ShowBoolSettings)
        {
            ShowSettings.SetActive(true);
        }
        else if(ShowBoolSettings == false)
        {
            ShowSettings.GetComponent<Animator>().Play("Hide");
            StartCoroutine(LoadingTime());
        }
    }
    public void ISQuit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        PlayerPrefs.SetString("Scene", "0");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void ISDailyReward()
    {
        ShowBoolDaily = !ShowBoolDaily;
        if (ShowBoolDaily)
        {
            ShowDaily.SetActive(true);
        }
        else if(ShowBoolDaily == false)
        {
            ShowDaily.SetActive(false);
        }
    }
    public void StartRoomIn()
    {
        PlayerPrefs.SetString("Mode", "");
        PlayerPrefs.SetString("Scene", "1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    IEnumerator LoadingTime()
    {
        yield return new WaitForSeconds(0.5f);
        ShowSettings.SetActive(false);
    }
    [Header("Boolean Manager")]
    internal bool ShowBoolSettings = false;
    internal bool ShowBoolDaily = false;
}
