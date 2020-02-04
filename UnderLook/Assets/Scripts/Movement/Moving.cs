using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float speed, gravity, jumpspeed;
    private float moveZ;
    public bool canMove = true, gravityApplied = true;

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
        //float moveZ = 0.0f;
        
        if ((Input.GetButton ("Jump") && characterController.isGrounded))
        {
            moveZ = jumpspeed;
        }
        moveZ -= gravity * Time.deltaTime;

        if (characterController.isGrounded)
            moveZ = 0;

        if (canMove)
            characterController.Move(transform.forward*moveX + transform.right*moveY); //time multiplié au carré
        if(gravityApplied)
            characterController.Move(transform.up * moveZ * Time.deltaTime);
    }
    

}