using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Basket : MonoBehaviour
{
    [SerializeField] private List<Transform> positions;
    private int posCounter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaceItemInsideBasket(GameObject obj)
    {
        if (posCounter >= positions.Count)
            posCounter = 0;


        obj.transform.DOJump(positions[posCounter].position,5,1,1f).OnComplete(()=> {

            obj.transform.parent = transform;
            obj.GetComponent<Rigidbody>().isKinematic = true;
           

        });
        posCounter++;



    }



}
