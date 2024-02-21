using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Knife : MonoBehaviour
{
    private Transform board;
    private const string slice = "Slice of Items";
    [SerializeField] private List<Transform> rigidBodiesPositions;
    [Header(slice)]
    [SerializeField] private float jumpHeight=1;
    [SerializeField] private float sliceStep = 1;
    [SerializeField] private float difference=0;
    int number=0;
    // Start is called before the first frame update
    void Start()
    {
        board = MaskMakingLevel.Instance.Board.transform.GetChild(0).GetChild(1).transform; 
    }

   
    Vector3 temp=Vector3.zero;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ChoppingItem")
        {
           
            if (collision.transform.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                //rigidbody.isKinematic = false;
                ParticleManager.Instance.PlayAnimation("Cutting Item", rigidbody.position,ItemsManager.Instance.selectedItem.color);
                Vector3 pos = rigidbody.transform.position - Vector3.right * (1+ sliceStep);
                if (rigidBodiesPositions.Count != 0)
                {
                    pos.x = rigidBodiesPositions[rigidBodiesPositions.Count - 1].position.x ;
                    pos += (1 + sliceStep) * Vector3.right;
                    //pos += (1 / (difference+1)) * Vector3.right;

                }


                //rigidbody.transform.DOJump(rigidbody.transform.position + Vector3.left * (1 + sliceStep) + Vector3.right * (difference * 0.2f), jumpHeight, 1, 0.125f).OnStart(() =>
                //{
                //    rigidbody.tag = "Untagged";
                //});
                rigidbody.transform.DOJump(pos /*+ Vector3.right * (difference * 0.2f)*/, jumpHeight, 1, 0.125f).OnStart(() => {
                    rigidbody.tag = "Untagged";
                });
                difference++;
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
