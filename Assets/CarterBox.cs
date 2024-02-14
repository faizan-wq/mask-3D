using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CarterBox : MonoBehaviour
{
    public List<Transform> positions;
    int number;

    public void NextPosition(Transform obj )
    {
        if (number >= positions.Count)
            number = 0;

        obj.GetComponent<Collider>().enabled = false;

        obj.transform.DOJump(positions[number].position,2,1,0.25f).OnComplete(()=> {

            obj.parent = transform.GetChild(0);
            obj.GetComponent<Rigidbody>().isKinematic = true;
        });
        number++;


    }
  
}
