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
    public Item(Item obj)
    {
        this.sprite=obj.sprite;
        this.prefab=obj.prefab;
        this.color=obj.color;
        this.itemType=obj.itemType;

    }
    


}

public enum Item_Type
{
    Good,
    Bad
}

