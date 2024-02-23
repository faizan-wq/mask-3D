using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Knife : MonoBehaviour
{
    private Transform board;
    private const string slice = "Slice of Items";
    [SerializeField] private List<Transform> rigidBodiesPositions;
    public MaskMakingLevel maskMakingLevel;
    [Header(slice)]
    [SerializeField] private float jumpHeight=1;
    [SerializeField] private float sliceStep = 1;
    [SerializeField] private float difference=0;
    int number=0;
    // Start is called before the first frame update
    void Start()
    {
        board = maskMakingLevel.Board.transform.GetChild(0).GetChild(1).transform; 
    }

   
    Vector3 temp=Vector3.zero;
    float divider = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ChoppingItem")
        {
           
            if (collision.transform.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                //rigidbody.isKinematic = false;
                ParticleManager.Instance.PlayAnimation("Cutting Item", rigidbody.position,ItemsManager.Instance.selectedItem.color);

            



                Vector3 pos = rigidbody.transform.position - Vector3.forward * (1+sliceStep)+Vector3.left*(divider);
                rigidbody.transform.DOJump(pos, jumpHeight, 1, 0.125f).OnStart(() => {
                    rigidbody.tag = "Untagged";
                });
                difference++;
                divider = 1- difference*0.1f;
                
                rigidBodiesPositions.Add(rigidbody.transform);
                StartCoroutine(waitAndExecute(rigidbody,board,0));
               
            }
         
        }
    }
  
    IEnumerator waitAndExecute(Rigidbody rigidbody, Transform parent, float wait)
    {
        yield return new WaitForSeconds(wait);
        ChangeParentAfterWait(rigidbody, parent);
        yield return new WaitForSeconds(0.25f);

        //rigidbody.GetComponent<Slice>().enabled = true;
    }
    private void ChangeParentAfterWait(Rigidbody rigidbody,Transform parent)
    {
       
        rigidbody.transform.parent = parent;
        

    }
    public void DisableKinematicsOfItemSlice()
    {
        for (int i = 0; i < board.childCount; i++)
        {
            if (board.GetChild(i).TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.isKinematic = false;
                rigidbody.AddForce(Vector3.right * 100);
               
            }
        }
    }



}
