using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskReachTarget : MonoBehaviour
{
    [HideInInspector] public bool maskReachedTarget=false;
    private void MaskReachedTargetPosition()
    {
        maskReachedTarget = true;
    }

}
