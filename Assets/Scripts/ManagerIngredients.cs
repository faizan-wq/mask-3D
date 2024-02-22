using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerIngredients : MonoBehaviour
{
    [Header("Controller Face Effecs")]
    public GameObject[] EffectShowUp;

    [Header("AllHair Sprites")]
    public Sprite IconOne;
    public Sprite IconTwo;

    [Header("Acnes Sprites")]
    public Sprite IconOneAcnes;
    public Sprite IconTwoAcnes;

    [Header("MushRooms Sprites")]
    public Sprite IconOneMushRooms;
    public Sprite IconTwoMushRooms;

    [Header("BlackAcnes Sprites")]
    public Sprite IconOneBlackAcnes;
    public Sprite IconTwoBlackAcnes;

    [Header("BigAcne Sprites")]
    public Sprite IconOneBigAcne;
    public Sprite IconTwoBigAcnes;
    void Awake()
    {



        //if(PlayerPrefs.GetString("Mode") == "")
        //{
        //    CurrentEffecs = Random.Range(0, EffectShowUp.Length);
        //    PlayerPrefs.SetString("Mode", EffectShowUp[CurrentEffecs].name);
        //}

        //  int value = PlayerPrefs.GetInt("Day");


        LevelChangeUponDay();






    }

    private void LevelChangeUponDay()
    {
        int value = PlayerPrefs.GetInt("Day")%9;

        switch (value)
        {
            case 0:
                CurrentEffecs =3;
               
                break;
            case 1:
                CurrentEffecs = 1;
                break;
            case 2:
                CurrentEffecs = 0;
                break;
            case 3:
                CurrentEffecs = 2;
                break;
            case 4:
                CurrentEffecs = 1;
                break;
            case 5:
                CurrentEffecs = 0;
                break;
            case 6:
                CurrentEffecs = 4;
                break;
            case 7:
                CurrentEffecs = 1;
                break;
            case 8:
                CurrentEffecs = 0;
                break;
            
            default:
                break;
        }
        PlayerPrefs.SetString("Mode", EffectShowUp[CurrentEffecs].name);

    }


    void Update()
    {
        if(PlayerPrefs.GetString("Mode") == "")
        {
            foreach(GameObject Mode in EffectShowUp) { Mode.SetActive(false); }
        }else
        {
            CheckActive();
            foreach (GameObject obj in EffectShowUp)
            {
                if ((PlayerPrefs.GetString("Mode") == obj.name))
                {
                    obj.SetActive(true);
                }
            }
        }
    }
    void CheckActive()
    {
        foreach(GameObject Effect in EffectShowUp)
        {
            if(Effect.activeSelf && Effect.name == "AllHair")
            {
                HomeScene Controller = FindAnyObjectByType<HomeScene>();
                Controller.IconOne.sprite = IconOne;
                Controller.IconTwo.sprite = IconTwo;
            }            
            if(Effect.activeSelf && Effect.name == "Acnes")
            {
                HomeScene Controller = FindAnyObjectByType<HomeScene>();
                Controller.IconOne.sprite = IconOneAcnes;
                Controller.IconTwo.sprite = IconTwoAcnes;
            }           
            if(Effect.activeSelf && Effect.name == "MushRooms")
            {
                HomeScene Controller = FindAnyObjectByType<HomeScene>();
                Controller.IconOne.sprite = IconOneMushRooms;
                Controller.IconTwo.sprite = IconTwoMushRooms;
            }            
            if(Effect.activeSelf && Effect.name == "BlackAcnes")
            {
                HomeScene Controller = FindAnyObjectByType<HomeScene>();
                Controller.IconOne.sprite = IconOneBlackAcnes;
                Controller.IconTwo.sprite = IconTwoBlackAcnes;
            }           
            if(Effect.activeSelf && Effect.name == "BigAcne")
            {
                HomeScene Controller = FindAnyObjectByType<HomeScene>();
                Controller.IconOne.sprite = IconOneBigAcne;
                Controller.IconTwo.sprite = IconTwoBigAcnes;
            }
        }
    }
    [Header("Integer Controller")]
    internal int CurrentEffecs;
}
