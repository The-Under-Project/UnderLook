using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spacePlay : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("space", !anim.GetBool("space"));
        }
    }
}
