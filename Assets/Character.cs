using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<CharacterProperties> characterProperties;
    public SkinnedMeshRenderer hairs;
    public List<GameObject> faceIssues;
    public bool allowFaceAppearance;
    private void Start()
    {
       if(allowFaceAppearance)
        {
            ApplyFaceIssue(PlayerPrefs.GetString("Mode"));
        }
      
    }



    public  void ChangeCharaterProperties()
    {
       

        int value = PlayerPrefs.GetInt(CharacterController.SelectedCharacter);

        switch (value)
        {
            case 0:
                ChangeAppearance(0);
                break;
            case 1:
                ChangeAppearance(1);
                break;
            case 2:
                ChangeAppearance(2);
                break;
            case 3:
                ChangeAppearance(3);
                break;
            case 4:
                ChangeAppearance(4);
                break;

            default:
                break;
        }

      



    }
    public void ChangeAppearance(int number)
    {
        foreach (var item in characterProperties)
        {
            item.LowerDress.SetActive(false);
            item.UpperDress.SetActive(false);
        }
        characterProperties[number].LowerDress.SetActive(true);
        characterProperties[number].UpperDress.SetActive(true);
        foreach (var item in hairs.materials)
        {
            item.color = characterProperties[number].HeadColor;
        }
    }
    public void ApplyFaceIssue(string issue)
    {
        switch (issue)
        {
            case "AllHair":
                faceIssues[0].SetActive(true);
                break;
            case "Acnes":
                faceIssues[1].SetActive(true);
                break;
            case "MushRooms":
                faceIssues[2].SetActive(true);
                break;
            case "BlackAcnes":
                faceIssues[3].SetActive(true);
                break;
            case "BigAcne":
                faceIssues[4].SetActive(true);

                break;


            default:
                break;
        }
    }




}
[Serializable]
public struct CharacterProperties
{
    public GameObject UpperDress;
    public GameObject LowerDress;
    public Color HeadColor;


}