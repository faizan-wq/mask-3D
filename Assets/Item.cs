using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public Sprite sprite;
    public GameObject prefab;
    public Color color;
    public Item_Type itemType;

}

public enum Item_Type
{
    Good,
    Bad
}

