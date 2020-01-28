using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddForce : MonoBehaviour
{
    public Rigidbody RB;
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.AddForce(new Vector3(0, 500, 500));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
