using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecteurWater : MonoBehaviour
{
    internal bool CheckStart = false;
    internal bool StartPool = false;
    void Update()
    {
        CreatorMilkUI Controller = FindAnyObjectByType<CreatorMilkUI>();
        CheckStart = Controller.StartMovingBarrel;
        if(CheckStart == true)
        {

        }
        else
        {
            Controller.ThrowUpWater.Stop();
            StartPool = true;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if(other.name == "PourMilkTask")
        {
            CreatorMilkUI Controller = FindAnyObjectByType<CreatorMilkUI>();
            if(Controller.StartMovingBarrel)
            {
                Controller.MovingFilling += 0.04f;
                if (StartPool)
                {
                    Controller.ThrowUpWater.Play();
                    StartPool = false;
                }
            }
        }
        else
        {
            if (StartPool == false)
            {
                CreatorMilkUI Controller = FindAnyObjectByType<CreatorMilkUI>();
                Controller.ThrowUpWater.Stop();
                StartPool = true;
            }
        }
    }
}
