using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    [SerializeField] private List<ParticleProperties> particles;
    




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
    public void PlayAnimation(string name, Vector3 position, Color color)
    {
        ParticleProperties particleProperties = GetParticleProperties(name);
        if (particleProperties == null)
            return;

        GameObject newparticle = Instantiate(particleProperties.particle.gameObject);
        newparticle.GetComponent<ParticleSystem>().startColor = color;
        for (int i = 0; i < newparticle.transform.childCount; i++)
        {
            if(newparticle.transform.GetChild(i).TryGetComponent<ParticleSystem>(out ParticleSystem particleSystem))
            {
                particleSystem.startColor = color;
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