using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    void Awake()
    {
        if(PlayerPrefs.GetString("FirstSettings") == "")
        {
            if(OnObj.activeSelf == true)
            {
                PlayerPrefs.SetString(this.name, "On");
                PlayerPrefs.SetString("FirstSettings", "Done");
            }
            if(OffObj.activeSelf == true)
            {
                PlayerPrefs.SetString(this.name, "Off");
                PlayerPrefs.SetString("FirstSettings", "Done");
            }
        }
    }
    void Start()
    {
        if(PlayerPrefs.GetString(this.name) == "On")
        {
            OffObj.SetActive(false);
            OnObj.SetActive(true);
        }
        else if(PlayerPrefs.GetString(this.name) == "Off")
        {
            OnObj.SetActive(false);
            OffObj.SetActive(true);
        }
    }
    public void SetToggle(bool SetDone)
    {
        if (SetDone)
        {
            PlayerPrefs.SetString(this.name, "On");
            OffObj.SetActive(false);
            OnObj.SetActive(true);
        }
        else if (!SetDone)
        {
            PlayerPrefs.SetString(this.name, "Off");
            OnObj.SetActive(false);
            OffObj.SetActive(true);
        }
    }
    [Header("Controller")]
    public GameObject OnObj;
    public GameObject OffObj;
    public GameObject IconObj;
}
