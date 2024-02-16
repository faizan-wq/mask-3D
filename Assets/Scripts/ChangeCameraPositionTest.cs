using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using TMPro;
public class ChangeCameraPositionTest : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera obj;
    [SerializeField] private CameraOffset offset;
    [SerializeField] private  TMP_InputField input;
    [SerializeField] private float cameraCurrentValue=0;
  
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
        
        StartCoroutine(ChangeCameraPositionAtCustomSpeed(value,0.05f));


        if (value == 3)
        {
            offset.ChangeRotation(1);
        }
        else
        {
            offset.ChangeRotation(0);
        }
    }
    
    private IEnumerator ChangeCameraPositionAtCustomSpeed(int value, float wait)
    {

        while (cameraCurrentValue<value)
        {

            cameraCurrentValue = Mathf.Clamp(cameraCurrentValue + 0.1f, 0, value);
            obj.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = cameraCurrentValue;
            yield return new WaitForSeconds(wait );
        }
        yield return null;
      
    }

  
}
