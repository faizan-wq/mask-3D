using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject AllHair;
    public GameObject Acnes;
    public GameObject MushRooms;
    public GameObject BlackAcnes;
    public GameObject BigAcne;

    [Header("Controller UI")]
    public GameObject CreatorMilkUI;

    void Awake()
    {
        //PlayerPrefs.SetString("Mode", "Acnes");
    }
    void Start()
    {
        if(PlayerPrefs.GetString("Mode") == "AllHair")
        {
            //ContainerUI.SetActive(true);
            AllHair.SetActive(true);
        }
        if (PlayerPrefs.GetString("Mode") == "Acnes")
        {
            ContainerUI.SetActive(true);
            CreatorMilkUI.SetActive(true);
            Acnes.SetActive(true);
        }
        if (PlayerPrefs.GetString("Mode") == "MushRooms")
        {
            ContainerUI.SetActive(true);
            MushRooms.SetActive(true);
        }
        if (PlayerPrefs.GetString("Mode") == "BlackAcnes")
        {
            ContainerUI.SetActive(true);
            BlackAcnes.SetActive(true);
        }
        if (PlayerPrefs.GetString("Mode") == "BigAcne")
        {
            //ContainerUI.SetActive(true);
            BigAcne.SetActive(true);
        }
    }
    [Header("UI Container")]
    public GameObject ContainerUI;
}
