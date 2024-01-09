using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Appel : MonoBehaviour
{
    internal bool IsAppel = false;
    internal bool StartJump = false;
    internal bool ControlleDirection = true;
    internal bool OtherStuff = true;
    internal bool CheckLoaded = true;
    public GameObject Baskest;

    void Update()
    {
        if (IsAppel)
        {
            if (StartJump)
            {
                transform.parent = Baskest.transform;
                if (ControlleDirection)
                {
                    TreeGrowUp up = FindAnyObjectByType<TreeGrowUp>();
                    float Value = up.JumpSpeed;
                    this.GetComponent<Rigidbody>().AddForce(transform.up * Value);
                    ControlleDirection = false;
                }
                else if (OtherStuff)
                {
                    transform.position = Baskest.transform.position;
                    if (CheckLoaded)
                    {
                        StartCoroutine(SetPosition());
                        CheckLoaded = false;
                    }
                   // OtherStuff = false;
                }
            }
        }
    }
    IEnumerator SetPosition()
    {
        yield return new WaitForSeconds(0.4f);
        OtherStuff = false;
        this.GetComponent<Rigidbody>().mass = 80f;
        yield return new WaitForSeconds(0.4f);
        //CheckLoaded = false;
        //Destroy(this.GetComponent<Rigidbody>());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.name == "Cart8")
        {
            IsAppel = false;
            Debug.Log("Done");
        }
    }
}
