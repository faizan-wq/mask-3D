using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    [SerializeField]private List<Vector3> rotation; 
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRotation(int number)
    {
        if (number >= rotation.Count)
            return;

        transform.rotation =Quaternion.Euler(rotation[number]) ;
    }


}
