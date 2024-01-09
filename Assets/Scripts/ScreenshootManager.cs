using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenshootManager : MonoBehaviour
{
    public AudioSource BtnClick;
    private void Start()
    {
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();

        foreach (Button button in buttons)
        {
            // Do something with the button
            button.onClick.AddListener(Clicked);
        }
    }
    void Clicked()
    {
        BtnClick.Play();
    }
}
