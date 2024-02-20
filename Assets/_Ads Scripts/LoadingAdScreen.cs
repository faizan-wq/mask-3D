using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAdScreen : MonoBehaviour
{

    public static LoadingAdScreen instance;
    public static bool isShowing=true;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        DontDestroyOnLoad(this.gameObject);
        HideLoadingAdScreen();
    }
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        StartCoroutine(HideScreenAfterWait(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator HideScreenAfterWait(float wait)
    {
        yield return new WaitForSecondsRealtime(wait);
        HideLoadingAdScreen();
    }

    public void ShowLoadingAdScreen(Action callback,bool check=false)
    {
        instance?.transform.GetChild(0).gameObject.SetActive(true);
        instance?.gameObject.SetActive(true);
        isShowing = true;
        StartCoroutine(waitAndExecute(callback,check));
        
    }
    public void ShowLoadingAdScreen(Action callback,Action second)
    {
        instance?.transform.GetChild(0).gameObject.SetActive(true);
        instance?.gameObject.SetActive(true);
        isShowing = true;
        StartCoroutine(waitAndExecute(callback,second));

    }
    public static void HideLoadingAdScreen()
    {
        isShowing = false;
        instance?.gameObject.SetActive(false);
    }

    IEnumerator waitAndExecute(Action callback,bool check=false)
    {
        if(!check)
        {
            yield return new WaitForSeconds(1);
            callback?.Invoke();
            HideLoadingAdScreen();
        }
        else
        {
            yield return new WaitForSeconds(1);
            callback?.Invoke();
        }
       
    }



    IEnumerator waitAndExecute(Action callback,Action second)
    {
        yield return new WaitForSeconds(1);
        callback?.Invoke();
        yield return new WaitForSeconds(1);
        second?.Invoke();
        HideLoadingAdScreen();
    }

}
