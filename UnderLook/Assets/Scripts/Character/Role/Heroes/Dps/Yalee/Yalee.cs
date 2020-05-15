using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class Yalee : Dps
    {
        [Header("Yalee")]
        public GameObject canvasUI;

        private Boolean cd = false;
        public float time1 = 10f;
        public float time2 = 3f;
        //public GameObject smokeEffect;
        public Camera cam;
        public float actualtime = 0f;
        public float cdtime = 3;

        private void Start()
        {

            actualtime = cdtime;
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
            if (actualtime <= 0)
            {
                GetComponent<Moving>().speed = speed;
                GetComponent<Moving>().jumpspeed = jumpspeed;
            }
            else
            {
                time();
            }

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
            this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
        }
        private void M2()
        {
            for (int i = 0; i < 3; i++)
            {
                this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
            }
        }

        private void Cap1()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown1 == 1 || canvasUI.GetComponent<UI>().rescue)
            {
                actualtime = cdtime;
                canvasUI.GetComponent<UI>().rescue = false;
                canvasUI.GetComponent<UI>().cap("one");
                this.GetComponent<Moving>().speed = speed * 2f;
                this.GetComponent<Moving>().jumpspeed = jumpspeed * 2f;

            }
        }

        private void Cap2()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");

                canvasUI.GetComponent<UI>().rescue = true;
            }
        }
        public void time()
        {
            if (actualtime > 0)
            {
                actualtime -= Time.deltaTime;
            }
        }
        /*
        private void Ulti()
        {
            var smokeEffectInstantiate = Instantiate(smokeEffect, transform.position, transform.rotation) as GameObject;

            Destroy(smokeEffectInstantiate, 20f);
        }*/
    }
}