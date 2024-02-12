using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Knife : MonoBehaviour
{
    private Transform board;
    private const string slice = "Slice of Items";
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

    // Update is called once per frame
    void Update()
    {
        
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
                rigidbody.transform.DOJump(rigidbody.transform.position + Vector3.left * (1 + sliceStep) + Vector3.right * (difference * 0.2f), jumpHeight, 1, 0.125f).OnStart(() => {
                    rigidbody.tag = "Untagged";
                });
                difference++;
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
               
            }
        }
    }



}
