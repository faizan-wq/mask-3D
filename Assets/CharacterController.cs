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



    // Start is called before the first frame update
    void Start()
    {
        LockAllAvatars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Methods

    private void LockAllAvatars()
    {
        foreach (var item in AvatarList)
        {
            item.interactable = false;
        }
    }

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

    }
  



    #endregion



}
