using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<CharacterProperties> characterProperties;
    public SkinnedMeshRenderer hairs;

    public  void ChangeCharaterProperties()
    {
        foreach (var item in characterProperties)
        {
            item.LowerDress.SetActive(false);
            item.UpperDress.SetActive(false);
        }

        int value = PlayerPrefs.GetInt(CharacterController.SelectedCharacter); 

        if (value >= 0)
        {
            ChangeAppearance(0);
        }

        if (value >= 4)
        {
            ChangeAppearance(1);
        }
        if (value >= 10)
        {
            ChangeAppearance(2);
        }
        if (value >= 15)
        {
            ChangeAppearance(3);
        }




    }
    private void ChangeAppearance(int number)
    {
        characterProperties[number].LowerDress.SetActive(false);
        characterProperties[number].UpperDress.SetActive(false);
        foreach (var item in hairs.materials)
        {
            item.color = characterProperties[number].HeadColor;
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