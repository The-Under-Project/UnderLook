using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingAi : MonoBehaviour
{
    public float speed;


    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;
    public bool left = true;
    CharacterController characterController;

    private void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (left)
        {
            Left();
        }
        else
        {
            Right();
        }
    }

    void Left()
    {
        Vector3 target = new Vector3(maxX, 1, maxZ); 
        transform.position = Vector3.MoveTowards(transform.position, target, speed);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            left = false;
        }
    }
    void Right()
    {
        Vector3 target = new Vector3(-maxX, 1, maxZ);
        transform.position = Vector3.MoveTowards(transform.position, target, speed);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            left = true;
        }
    }

}
