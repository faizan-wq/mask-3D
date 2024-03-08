using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class SplahLoadingScreen : MonoBehaviour
{
    public Image loading;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = loading.transform.GetChild(0).GetComponent<Text>();
        loading.DOFillAmount(1, 3).OnUpdate(()=> {

            text.text = (Convert.ToInt32(loading.fillAmount * 100)).ToString()+"%";


        }).OnComplete(() => {
            SceneManager.LoadScene("Home");
        });
    }

   
}
