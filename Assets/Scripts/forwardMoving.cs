using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardMoving : MonoBehaviour
{
    internal bool Done = false;
    void FixedUpdate()
    {
        if (Done)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -6);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Done = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        Done = false;
    }
}
