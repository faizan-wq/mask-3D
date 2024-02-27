using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    [SerializeField] private List<ParticleProperties> particles;
    public SoundManager soundManager;





    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayAnimation(string name,Vector3 position)
    {
        ParticleProperties particleProperties =  GetParticleProperties(name);
        if (particleProperties == null)
            return;

        GameObject newparticle = Instantiate(particleProperties.particle.gameObject);
        if (position != Vector3.zero)
            newparticle.transform.position = position;

    }
    public void PlayAnimation(string name, bool needToCreate=false)
    {
        ParticleProperties particleProperties = GetParticleProperties(name);
        if (particleProperties == null)
            return;

        if(!needToCreate)
        {
            particleProperties.particle.Play();
            return;
        }
        GameObject newparticle = Instantiate(particleProperties.particle.gameObject);
    
           

    }
    public void PlayAnimation(string name, Vector3 position, Color color)
    {
        ParticleProperties particleProperties = GetParticleProperties(name);
        if (particleProperties == null)
            return;

        GameObject newparticle = Instantiate(particleProperties.particle.gameObject);
        float alphaValue = newparticle.GetComponent<ParticleSystem>().startColor.a;
        color.a = alphaValue;
        newparticle.GetComponent<ParticleSystem>().startColor = color;
        
        for (int i = 0; i < newparticle.transform.childCount; i++)
        {
            if(newparticle.transform.GetChild(i).TryGetComponent<ParticleSystem>(out ParticleSystem particleSystem))
            {
                particleSystem.startColor = color;
            }
            for (int j = 0; j < newparticle.transform.GetChild(i).childCount; j++)
            {
                if (newparticle.transform.GetChild(i).GetChild(j).TryGetComponent<ParticleSystem>(out ParticleSystem particleSystemChild))
                {
                    particleSystemChild.startColor = color;
                }
            }
        }
        newparticle.transform.position = position;

    }


    private ParticleProperties GetParticleProperties(string name)
    {
        foreach (var item in particles)
        {
            if(item.name==name)
            {
                return item;
            }
        }


        return null;
    }



}
[Serializable]
public class ParticleProperties
{
    public string name;
    public ParticleSystem particle;
  
}