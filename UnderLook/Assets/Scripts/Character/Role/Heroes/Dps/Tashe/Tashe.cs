using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class Tashe: Dps
    {
        [Header("Tashe")]

        public GameObject canvasUI;
        public Camera cam;
        public GameObject sphere;
        public GameObject shotPoint;
        public GameObject flare;
        private int power = 15;

        private Boolean isInUlt = false;




        private void Start()
        {

            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;

            cam = GetComponentInChildren<Camera>();

            canvasUI.GetComponent<UI>().hasShield = false;
            canvasUI.GetComponent<UI>().hasThreeCapacities = false;
            canvasUI.GetComponent<UI>().maxHP = hpmax;

        }
        void FixedUpdate()
        {

            if (hp <= 0)
            {
                //Debug.Log("Dead");
                //dead();
            }

            canvasUI.GetComponent<UI>().CurrentHP = hp;
        }

        //-----------------------------
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

        }


        private void M1()
        {
            if (!isInUlt)
            {
                //met le tire ici
            }
            else if (isInUlt)
            {
                GameObject flareInstance = Instantiate(flare, shotPoint.transform.position, Quaternion.identity) as GameObject;
                Vector3 force = transform.forward;
                force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * cam.transform.rotation.eulerAngles.x) * 2f, force.z);
                force *= power;
                flareInstance.GetComponent<Rigidbody>().AddForce(force);
                isInUlt = false;
            }
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
            isInUlt = true;
        }
    }
}
