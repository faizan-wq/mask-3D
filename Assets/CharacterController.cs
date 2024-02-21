using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterController : MonoBehaviour
{

    [SerializeField] private List<Button> AvatarList;
    [SerializeField] private Character character;  


    public const string  SelectedCharacter= "SelectedCharacter";



  
    void Start()
    {
        //LockAllAvatars();
        UnlockCharacters();
        ChangeCharacterAppearance();
    }

  
    #region Methods

    //private void LockAllAvatars()
    //{
    //    foreach (var item in AvatarList)
    //    {
    //        item.interactable = false;
    //    }
    //}

    public static void CreateSelectedAvatar(int number=0)
    {
        PlayerPrefs.SetInt(SelectedCharacter,number);
        
    }


    public void UnlockCharacters()
    {
        int value = PlayerPrefs.GetInt("Day");

        if (value >= 0)
        {
            AvatarListButtonFunc(0);
        }

        if (value >= 4)
        {
            AvatarListButtonFunc(1);
        }
        if (value >= 10)
        {
            AvatarListButtonFunc(2);
        }
        if (value >= 15)
        {
            AvatarListButtonFunc(3);
        }
        //if (value >= 20)
        //{
        //    AvatarListButtonFunc(4);
        //}


    }

    private void AvatarListButtonFunc(int number)
    {
        if (AvatarList.Count == 0)
            return;
        AvatarList[number].interactable = true;
        AvatarList[number].transform.GetChild(0).gameObject.SetActive(false);
        AvatarList[number].onClick.AddListener(() =>
        {


            CreateSelectedAvatar(number);
            ChangeCharacterAppearance();

        });
    }

    public void ChangeCharacterAppearance()
    {
        character.ChangeCharaterProperties();
        int value = PlayerPrefs.GetInt(CharacterController.SelectedCharacter);
        switch (value)
        {
            case 0:
                AvatarList[0].transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 1:
                AvatarList[1].transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                AvatarList[2].transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                AvatarList[3].transform.GetChild(1).gameObject.SetActive(true);
                break;
            default:
                break;
        }



    }
  



    #endregion



}
