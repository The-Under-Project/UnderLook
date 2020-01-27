using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float speed, gravity, jumpspeed;
    [SerializeField] private float rapport;
    public float t;
    private float moveZ;

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
        Time.timeScale = t;
        float moveX = Input.GetAxis("Vertical") *  speed*Time.deltaTime;
        float moveY = Input.GetAxis("Horizontal") *  speed*Time.deltaTime;
        //float moveZ = 0.0f;
        
        if ((Input.GetButton ("Jump") && characterController.isGrounded))
        {
            moveZ = jumpspeed;
        }
        moveZ -= gravity * Time.deltaTime * rapport;
        
        characterController.Move(transform.up * moveZ * Time.deltaTime + transform.forward*moveX + transform.right*moveY); //time multiplié au carré
        
    }
    

}