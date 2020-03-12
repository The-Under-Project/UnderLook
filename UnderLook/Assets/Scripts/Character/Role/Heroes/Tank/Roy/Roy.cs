using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Roy : Tank
{
    [Header("Roy")]
    public GameObject canvasUI;
    public float cameraFOV;
    public Camera cam;
    protected int shieldLife;
    protected int shieldRecovery;

    void Start()
    {
        GetComponent<Moving>().speed = speed;
        GetComponent<Moving>().jumpspeed = jumpspeed;


        cam = GetComponentInChildren<Camera>();
        cameraFOV = cam.fieldOfView;

        canvasUI.GetComponent<UI>().hasShield = true;
        canvasUI.GetComponent<UI>().hasThreeCapacities = false;
        canvasUI.GetComponent<UI>().maxHP = hpmax;
        shieldLife = 100;
        shieldRecovery = 10;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            M1();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            M2();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cap1();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cap2();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Ulti();
        }
    }

    private void M1()
    {
        
    }

    private void M2()
    {
        
    }

    private void Cap1()
    {
        
    }

    private void Cap2()
    {
        
    }

    private void Ulti()
    {
        
    }



}
