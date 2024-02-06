using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskTakingOff : MonoBehaviour
{
    [HideInInspector] public bool MasktakingOffCheck=false;
    private void MaskTakingOffComplete ()
    {
        MasktakingOffCheck = true;
    }
}
