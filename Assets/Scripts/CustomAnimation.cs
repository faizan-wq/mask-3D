using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimation : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
