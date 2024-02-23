using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GamePlayScene : MonoBehaviour
{
    [Header("UI")]
    public GameObject FinishUI;
    public GameObject FinishScene;
    public GameObject SkipBtn;

    void Update()
    {
        RotatorPoint.transform.Rotate(0, +0.3f,0);CashhUI.text = "" + PlayerPrefs.GetInt("Cash");
        CashUIFinish.text = "" + PlayerPrefs.GetInt("Cash");
    }
    public void SetSceneFinish()
    {
        FinishUI.GetComponent<Animator>().Play("Hide");
        StartCoroutine(LoadingNextScene());
    }
    IEnumerator LoadingNextScene()
    {
        yield return new WaitForSeconds(0.3f);
        FinishUI.SetActive(false);
        FinishScene.SetActive(true);
        yield return new WaitForSeconds(2f);
        SkipBtn.SetActive(true);
    }
    public GameObject RotatorPoint;

    [Header("UI Canvas Controller")]
    public Text CashhUI;
    public Text CashUIFinish;
    public Text CashUIRoom;
}
