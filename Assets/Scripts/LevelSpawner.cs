using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class LevelSpawner : MonoBehaviour
{
    public GameObject AllHair;
    public GameObject Acnes;
    public GameObject MushRooms;
    public GameObject BlackAcnes;
    public GameObject BigAcne;

    [Header("Controller UI")]
    public GameObject CreatorMilkUI;

    public string levelName= "AllHair";
    public Character character;


    void Awake()
    {
        //PlayerPrefs.SetString("Mode", "Acnes");
    }
    void Start()
    {
        //if (PlayerPrefs.GetString("Mode") == "AllHair")
        //{
        //    AllHair.SetActive(true);
        //    character?.gameObject.SetActive(true);
        //}
        //if (PlayerPrefs.GetString("Mode") == "Acnes")
        //{
        //    ContainerUI.SetActive(true);
        //    CreatorMilkUI.SetActive(true);
        //    Acnes.SetActive(true);
        //}
        //if (PlayerPrefs.GetString("Mode") == "MushRooms")
        //{
        //    ContainerUI.SetActive(true);
        //    MushRooms.SetActive(true);
        //}
        //if (PlayerPrefs.GetString("Mode") == "BlackAcnes")
        //{
        //    ContainerUI.SetActive(true);
        //    BlackAcnes.SetActive(true);
        //}
        //if (PlayerPrefs.GetString("Mode") == "BigAcne")
        //{

        //    BigAcne.SetActive(true);
        //    character?.gameObject.SetActive(true);
        //}
        character.ChangeAppearance(PlayerPrefs.GetInt(CharacterController.SelectedCharacter));
        switch (levelName)
        {
            case "AllHair":
                AllHair.SetActive(true);
                character?.gameObject.SetActive(true);
                break;
            case "Acnes":
                ContainerUI.SetActive(true);
                CreatorMilkUI.SetActive(true);
                Acnes.SetActive(true);
                break;
            case "MushRooms":
                ContainerUI.SetActive(true);
                MushRooms.SetActive(true);
                break;
            case "BlackAcnes":
                ContainerUI.SetActive(true);
                BlackAcnes.SetActive(true);
                break;
            case "BigAcne":

                BigAcne.SetActive(true);
                character?.gameObject.SetActive(true);
                break;

            default:
                break;
        }



    }
    [Header("UI Container")]
    public GameObject ContainerUI;
}
