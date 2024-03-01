using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleAAfterWait : MonoBehaviour
{


    public bool deathEffectStart = false;
    private ParticleSystem particleEffect;
    private void Awake()
    {
        particleEffect = GetComponent<ParticleSystem>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(deathEffectStart)
        {
            if(!particleEffect.isPlaying)
            {
                particleEffect.Play();
            }

        }
    }
}
