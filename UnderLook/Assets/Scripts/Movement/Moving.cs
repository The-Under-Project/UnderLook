using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float speed, gravity, jumpspeed;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
        
    }

    void Update()
    {
        MovementPlayer();
    }
    void MovementPlayer()
    {
        float moveX = Input.GetAxis("Vertical") *  speed*Time.deltaTime;
        float moveY = Input.GetAxis("Horizontal") *  speed*Time.deltaTime;
        float moveZ = 0.0f;
        
        if ((Input.GetButton ("Jump") && characterController.isGrounded))
        {
            moveZ = jumpspeed;
        }
        moveZ -= gravity * Time.deltaTime;
        
        characterController.Move(transform.up * moveZ * Time.deltaTime + transform.forward*moveX + transform.right*moveY);
        
    }
    

}