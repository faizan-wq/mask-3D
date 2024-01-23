using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using TMPro;
public class ChangeCameraPositionTest : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera obj;
    [SerializeField] private CameraOffset offset;
    [SerializeField] private  TMP_InputField input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangeTrack()
    {
        int value= int.Parse(input.text);
        obj.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = value;
        if(value==3)
        {
            offset.ChangeRotation(1);
        }
        else
        {
            offset.ChangeRotation(0);
        }
    }
    public void ChangeTrack(int value)
    {
       
        obj.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = value;
        if (value == 3)
        {
            offset.ChangeRotation(1);
        }
        else
        {
            offset.ChangeRotation(0);
        }
    }
}
