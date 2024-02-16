using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingItem : MonoBehaviour
{
    SkinnedMeshRenderer meshShape;
    private float currentValue;
    private Rigidbody rigidbody;
    private void Awake()
    {
        meshShape = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision:"+collision.gameObject.name);

        if (collision.gameObject==MaskMakingLevel.Instance.hammerController.gameObject
            && collision.impulse.magnitude>=2f )
        {
           

            Debug.Log("Collided with Hammer");
            currentValue =Mathf.Clamp(currentValue+ 20f,0,100f);
            meshShape.SetBlendShapeWeight(0, currentValue);
        }
    }
}
