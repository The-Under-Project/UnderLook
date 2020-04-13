using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Player;
using UnityEngine;
using DG.Tweening;

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

    [Header("Grappin")]
    public GameObject grap;
    public float timergrap;
    public float cooldownM2;
    public Collision collision;
    public float forceofGrap;
    public bool inMotion;
    public Boolean cd = false;
    private float time1 = 20f;

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
        if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1 && !inMotion)
        {
            canvasUI.GetComponent<UI>().cap("two");
            inMotion = true;
            GameObject grappin = Instantiate(grap, transform.position, Quaternion.identity) as GameObject;
            collision = grappin.GetComponent<Collision>();
            grappin.GetComponentInChildren<Rigidbody>().useGravity = false;
            grappin.GetComponentInChildren<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * forceofGrap);
            if (collision.gameObject.CompareTag("Player"))
            {
                RetourGrappin(collision.gameObject);
                
            }
            else
            {

            }
        }
        
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
        float hpSave = canvasUI.GetComponent<UI>().CurrentHP;
        canvasUI.GetComponent<UI>().CurrentHP = 1000f;
        float percentageCooldown1 = 0;
        Boolean cdRefresh1 = true;
        Sequence s = DOTween.Sequence();
        if(!cd)
            s.Append(DOTween.To(() => percentageCooldown1, x => percentageCooldown1 = x, 1, time1));
        else
            percentageCooldown1 = 1;
        canvasUI.GetComponent<UI>().CurrentHP = hpSave;
    }

   

    public void RetourGrappin(GameObject foe)
    {
        foe.GetComponentInChildren<Moving>().canMove = false;
        foe.GetComponent<Rigidbody>().velocity = this.transform.TransformDirection(-Vector3.forward * forceofGrap);
    }
    
    
}
