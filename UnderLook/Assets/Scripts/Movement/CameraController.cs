using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private string InputHorizontalAxis,  InputVerticalAxis;
    [SerializeField]private float SpeedRotate = 150f;
    [SerializeField] private Transform BodyPlayer;
    private float ClampX;
    public bool canmovevision = true;

    // Start is called before the first frame update
    void Start()
    {
        ClampX = 0.0f;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if(canmovevision)
            CameraMove();
    }


    // FixedUpdate is called once per frame after every other Update
    void CameraMove()
    {
        float mouseX =Input.GetAxis( InputHorizontalAxis )* SpeedRotate;
        float mouseY = Input.GetAxis(InputVerticalAxis)* SpeedRotate;

        ClampX += mouseY;
        if(ClampX>90.0f)
        {
            mouseY = 0.0f;
            ClampX = 90.0f;
            FixClamping(270.0f);

        }
        else if (ClampX<-90.0f)
        {
            mouseY = 0.0f;
            ClampX = -90.0f;
            FixClamping(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY );
        BodyPlayer.Rotate(Vector3.up * mouseX );
        
    }
    private void FixClamping (float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
