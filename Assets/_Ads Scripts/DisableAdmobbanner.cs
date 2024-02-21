using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAdmobbanner : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;
   

    private bool otherpanelsAreOff()
    {
        foreach (var item in panels)
        {
            if (item.activeInHierarchy)
                return false;
        }
        return true;
    }
}
