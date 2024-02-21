using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimation : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particles;
 
    private void PlayAnimation()
    {
        foreach (var item in particles)
        {
           
            item.Play();
        }
    }
    private void StopAnimation()
    {
        foreach (var item in particles)
        {
            item.loop = false;
           
        }
    }
}
