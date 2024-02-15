using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMask : MonoBehaviour
{
    [HideInInspector] public bool faceTakOffCheck = false;
    private void FaceTakeOffComplete()
    {
        faceTakOffCheck = true;
    }
}
