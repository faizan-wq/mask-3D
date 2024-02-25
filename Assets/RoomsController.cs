using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class RoomsController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera virtualCameraSecond;

    [Header("Input Limits")]
    [Space]
    public float mouseX_Min;
    public float mouseX_Max;
    public float mouseY_Min;
    public float mouseY_Max;
    [Header("Mouse Starting Pos")]
    [Space]
    public float mouseX;
    public float mouseY;
    public float mouseZoom;
    float tempX=0, tempY;
    [Header("Mouse Starting Pos")]
    [Space]
    public float HorizontalSpeed;
    public float VerticalSpeed;
    
    public bool allowCameraMovement = false;

   



    private void ChangeCameraPositionHorizontaly()
    {
        tempX = Input.GetAxis("Mouse X");
        
        mouseX = Mathf.Clamp(mouseX + tempX* HorizontalSpeed, mouseX_Min, mouseX_Max);
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = mouseX;
    }
    private void ChangeCameraPositionVertically()
    {
        tempY = Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY + tempY*VerticalSpeed, mouseY_Min, mouseY_Max);
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathOffset.y= mouseY;
    }

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;

    
    private void ZoomEffect()
    {
        //mouseZoom = Mathf.Clamp(mouseZoom + Input.GetAxis("Mouse ScrollWheel")*10, 40, 90);
        //virtualCamera.m_Lens.FieldOfView = mouseZoom;


        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                mouseZoom = Mathf.Clamp(mouseZoom + zoomModifier * 10, 40, 90);
                virtualCamera.m_Lens.FieldOfView = mouseZoom;
            }
           
            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                mouseZoom = Mathf.Clamp(mouseZoom - zoomModifier * 10, 40, 90);
                virtualCamera.m_Lens.FieldOfView = mouseZoom;
            }
              

        }

      

    }
    public void SetSecondVirtualcameratarget(Transform trans, int cameraPos) {
        

        StartCoroutine(ResetCameraPriority(trans,cameraPos));

    }
    private IEnumerator ResetCameraPriority(Transform trans, int cameraPos)
    {
        allowCameraMovement = false;
        virtualCameraSecond.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = cameraPos;
        virtualCameraSecond.LookAt = trans;
        yield return new WaitForSeconds(1);
        virtualCameraSecond.Priority = 20;
        yield return new WaitForSeconds(3);
        allowCameraMovement = true;
        virtualCameraSecond.Priority = 0;

    }



    private void Update()
    {
        if (!allowCameraMovement)
            return;

        ChangeCameraPositionHorizontaly();
        ChangeCameraPositionVertically();
        ZoomEffect();

    }


}

