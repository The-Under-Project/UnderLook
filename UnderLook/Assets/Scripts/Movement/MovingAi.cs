using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAi : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;
    [SerializeField] private float speed;
    CharacterController characterController;
    private Vector3 rotationvision;
    private int nomrederotation = 0;
    private void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x >= maxX && transform.position.z < maxZ )
        {
            
            //characterController.transform.rotation = Quaternion.Euler(rotationvision);
            characterController.Move(transform.forward * speed * Time.deltaTime);
        }
        else if (transform.position.z >= maxZ && transform.position.x >-maxX)
        {
            
            //characterController.transform.rotation = Quaternion.Euler(rotationvision);
            characterController.Move(transform.right * (-speed) * Time.deltaTime);

        }
        else if (transform.position.x <= -maxX && transform.position.z > -maxZ)
        { 
            //characterController.transform.rotation = Quaternion.Euler(rotationvision);
            characterController.Move(transform.forward * (-speed) * Time.deltaTime);
        }
        else if (transform.position.z <= -maxZ && transform.position.x < maxX)
        {
            //characterController.transform.rotation = Quaternion.Euler(rotationvision);
            characterController.Move(transform.right * speed * Time.deltaTime);
        }
    }
}
