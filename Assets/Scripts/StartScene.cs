using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class StartScene : MonoBehaviour
{
    [Header("Text Show")]
    public Text ValuePrecent;

    private void OnEnable()
    {
        LoadingProgress.fillAmount = 0;
        
    }

    private void Start()
    {
      
        LoadingProgress.DOFillAmount(1, 3).OnUpdate(() => {

            ValuePrecent.text = (Convert.ToInt32(LoadingProgress.fillAmount * 100)).ToString() + "%";


        }).OnComplete(() => {
            SceneChange();
        });

    }
    private void SceneChange()
    {
        if (PlayerPrefs.GetString("Scene") == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else if (PlayerPrefs.GetString("Scene") != "")
        {
            SceneManager.LoadScene(3);

        }
    }
    [Header("UI")]
    public Image LoadingProgress;

}
