using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkBacking : MonoBehaviour
{
    internal bool SetVec = true;
    internal bool Once = true;
    internal bool CheckDone = true;
    void Start()
    {
        CreatorMilkUI Controller = FindAnyObjectByType<CreatorMilkUI>();
        Controller.MovingUI += 0.2f;
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
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            CheckDone = false;
        }
    }
}
