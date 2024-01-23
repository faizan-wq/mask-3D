using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Transform board;
    // Start is called before the first frame update
    void Start()
    {
        board = MaskMakingLevel.Instance.Board.transform.GetChild(0).GetChild(1).transform; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ChoppingItem")
        {
            Debug.Log("collision gameObject"+collision.gameObject.name);
            if (collision.transform.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.isKinematic = false;
                rigidbody.tag = "Untagged";
                StartCoroutine(waitAndExecute(rigidbody,board,0));
               ;
            }
         
        }
    }
    IEnumerator waitAndExecute(Rigidbody rigidbody, Transform parent, float wait)
    {
        yield return new WaitForSeconds(wait);
        DisableConnectedObjectAfterWait(rigidbody, parent);
        yield return new WaitForSeconds(0.25f);

        rigidbody.GetComponent<Slice>().enabled = true;
    }
    private void DisableConnectedObjectAfterWait(Rigidbody rigidbody,Transform parent)
    {
       
        rigidbody.transform.parent = parent;
        

    }


}
