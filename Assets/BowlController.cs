using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    [SerializeReference] private SkinnedMeshRenderer water;
    [HideInInspector] public Color colorOfPaste;
    private void Start()
    {
        bendValue = new WaterShapeProperties[3];
        for (int i = 0; i < bendValue.Length; i++)
        {
            bendValue[i].blendShapeTimer = water.GetBlendShapeWeight(0);
        }
    }



    private void Update()
    {
        if (!startAnimation)
            return;

        WaterDisplacemnet();
    }

    #region Pouring

    [SerializeField] private WaterShapeProperties[] bendValue;
    public bool startAnimation = false;

    public void ChangeColorOfwater(Color color)
    {
        foreach (var item in water.materials)
        {
            item.color = color;
        }

    }




    private void WaterDisplacemnet()
    {
        waterEffect(0, 10);
        waterEffect(1, 10);
        waterEffect(2, 10);
    }

    private void waterEffect(int blendShapeIndex, float speed)
    {

        if (bendValue[blendShapeIndex].blendShapeTimer >= 25)
        {
            bendValue[blendShapeIndex].direction = -1;
        }
        else if (bendValue[blendShapeIndex].blendShapeTimer <= 0)
        {
            bendValue[blendShapeIndex].direction = 1;
        }

        bendValue[blendShapeIndex].blendShapeTimer += bendValue[blendShapeIndex].direction * Time.deltaTime * speed;

        water.SetBlendShapeWeight(blendShapeIndex, bendValue[blendShapeIndex].blendShapeTimer);



    }

    #endregion

    #region Mixing
    private float colorContrastLimit = 0.5f;
    private float colorContrast;
    [HideInInspector] public bool checkColorChangedCompletely;
    
    [HideInInspector]
    public void UpdateColorChangingEffect()
    {
        if (checkColorChangedCompletely)
            return;


        colorContrast = Mathf.Clamp(colorContrast + Time.deltaTime/10, 0, 0.3f);
        MaskMakingLevel.Instance.EnableTaskPoint(3, colorContrast/0.3f);
        ChangeWaterEffect(MaskMakingLevel.Instance.bottleController.selectedBottle.color, ItemsManager.Instance.selectedItem.color, colorContrast);
        if (colorContrast>=0.3f)
        {
            checkColorChangedCompletely = true;
        }
       


    }

    private void ChangeWaterEffect(Color a, Color b, float value)
    {
       
        ApplyColorToALLMaterialsOfBowlWater(Color.Lerp(a, b, colorContrast));



    }

    private void ApplyColorToALLMaterialsOfBowlWater(Color color)
    {
        color.a = 0.7f;
        foreach (var item in water.materials)
        {
            item.color = color;
        }
        colorOfPaste = color;
    }


    #endregion






}
[Serializable]
public struct WaterShapeProperties
{
    public float blendShapeTimer;
    public int direction;
}
