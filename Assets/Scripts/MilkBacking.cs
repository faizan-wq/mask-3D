using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkBacking : MonoBehaviour
{
    internal bool SetVec = true;
    internal bool Once = true;
    internal bool CheckDone = true;
    CreatorMilkUI creatorMilkUI;
    void Start()
    {
        CreatorMilkUI Controller = FindAnyObjectByType<CreatorMilkUI>();
        Controller.MovingUI += 0.2f;
        creatorMilkUI = GameObject.FindObjectOfType<CreatorMilkUI>();
    }
    void FixedUpdate()
    {
        if(SetVec && Once)
            this.GetComponent<Rigidbody>().AddForce(-transform.forward * 30);
    }
    void OnCollisionEnter(Collision collision)
    {
        SetVec = true;
    }
    void OnCollisionExit(Collision collision)
    {
        SetVec = false;
        Once = false;
        if (CheckDone)
        {

            creatorMilkUI.BoxAnimated.transform.GetComponent<CarterBox>().NextPosition(transform);

          
            
            CheckDone = false;
        }
    }
}
