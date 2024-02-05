using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPaste : MonoBehaviour
{
    [HideInInspector] public bool maskIsPastingComplete;

    private void MaskPastingIsComplete()
    {
        maskIsPastingComplete = true;
    }
}
