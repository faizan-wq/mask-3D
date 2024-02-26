using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StartScene : MonoBehaviour
{
    [Header("Text Show")]
    public Text ValuePrecent;
    public Text LevelLoaderSynce;

    private void Start()
    {
        LoaderTime = Levels.Length - 1;
        LoadingProgress.fillAmount = 0f;
    }
    private void Update()
    {
        if(LoadingProgress.fillAmount < 1 && LoaderTime > 0)
        {
            LoadingProgress.fillAmount += Time.deltaTime / 5;
            ValuePrecent.text = "" + (int)(LoadingProgress.fillAmount * 100) + "%";
            LevelLoaderSynce.text = Levels[LoaderTime];
        }
        else if(LoadingProgress.fillAmount == 1 && LoaderTime > 0)
        {
            if (PlayerPrefs.GetString("Scene") == "") { LoadingTime = Random.Range(1, 7); }
            else { LoadingTime = Random.Range(0.1f, 0.5f); }
            LoaderTime -= 1;
            LoadingProgress.fillAmount = 0f;
        }
        else if (CheckLoaded == false)
        {
            LoadingProgress.fillAmount = 1f;
            if(PlayerPrefs.GetString("Scene") == "")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                CheckLoaded = true;
            }else if(PlayerPrefs.GetString("Scene") != "")
            {
                SceneManager.LoadScene(3);
                CheckLoaded = true;
            }
        }
    }
    [Header("UI")]
    public Image LoadingProgress;

    [Header("Integer Controller")]
    internal int LoaderTime = 0;
    internal float LoadingTime = 1;

    [Header("Checker Strings")]
    public string[] Levels = new string[4];

    [Header("Boolean Manager")]
    internal bool CheckLoaded = false;
}
