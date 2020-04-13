using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Player;
using UnityEngine;

public class Roy : Tank
{
    [Header("Roy")]
    public GameObject canvasUI;
    public float cameraFOV;
    public Camera cam;
    new private int shieldLife;
    new private int shieldRecovery;
    new private int hp;
    private float time = 15f;
    private int power = 15;
    public GameObject shield;
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
        hp = hpmax;
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
        this.GetComponentInChildren<Weapon.WeaponSniper>().Shoot();
    }

    private void M2()
    {
        
    }

    private void Cap1()
    {
        GameObject shootedShield = Instantiate(shield, transform.position,Quaternion.identity) as GameObject;
        shootedShield.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
        shootedShield.SendMessage("SetColor", this.GetComponent<TeamColor>().enemieColor);
        Destroy(shootedShield, time);
    }

    private void Cap2()
    {
        GetComponentInChildren<Moving>().capacity = true;
    }

    private void Ulti()
    {
        int hpSave = getHp();
        setHp(1000);
    }

    public int getHp()
    {
        return hp;
    }

    public void setHp(int newHp)
    {
        hp = newHp;
    }
    
}
