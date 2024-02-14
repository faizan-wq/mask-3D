using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardMoving : MonoBehaviour
{
    internal bool Done = false;
    Rigidbody rigidbody;
    MachineColla machineCola;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        machineCola = GameObject.FindObjectOfType<MachineColla>();
    }
    void FixedUpdate()
    {

        if (Done)
        {
            rigidbody.velocity = new Vector3(0, 0, -20);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Done = true;
    }
    private void OnCollisionExit(Collision collision)
    {

        //rigidbody.AddForce(Vector3.down * 1000);
        machineCola.MachineBox.GetComponent<CarterBox>().NextPosition(transform);
      //  Invoke(nameof(StopPysicsEffect), 2);
      
      
      

        Done = false;
    }
    private void StopPysicsEffect()
    {
        
        rigidbody.isKinematic = true;
        transform.parent = machineCola.MachineBox.transform.GetChild(0);

    }
}
