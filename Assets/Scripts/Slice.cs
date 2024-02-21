using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "board")
        {
            if (collision.transform.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.isKinematic = true;
            }
        }
    }
}
