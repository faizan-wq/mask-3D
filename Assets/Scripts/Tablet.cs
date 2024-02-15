using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    [HideInInspector] public bool tabletInsideMachne;
    [HideInInspector] public bool tabletParticles;

    public void EnableTabletInsidemachine()
    {
        tabletInsideMachne = true;
    }
    public void EnableTabletParticles()
    {
        tabletParticles = true;
    }


}
