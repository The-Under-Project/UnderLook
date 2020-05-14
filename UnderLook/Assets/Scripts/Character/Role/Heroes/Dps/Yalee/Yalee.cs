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
            this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
        }
        private void M2()
        {
            for (int i = 0; i < 3; i++)
            {
                Sequence s = DOTween.Sequence();
                this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
                float percentageCooldown1 = 0;
                if (!cd)
                    s.Append(DOTween.To(() => percentageCooldown1, x => percentageCooldown1 = x, 1, time2));
                else
                    percentageCooldown1 = 1;
            }
        }

        private void Cap1()
        {
            canvasUI.GetComponent<UI>().cap("one");
            this.GetComponent<Moving>().speed = speed * 1.5f;
            this.GetComponent<Moving>().jumpspeed = jumpspeed * 1.5f;

            Sequence s = DOTween.Sequence();
            float percentageCooldown1 = 0;
            if (!cd)
                s.Append(DOTween.To(() => percentageCooldown1, x => percentageCooldown1 = x, 1, time1));
            else
                percentageCooldown1 = 1;
            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;
        }

        private void Cap2()
        {
            canvasUI.GetComponent<UI>().cap("two");
        }
        /*
        private void Ulti()
        {
            var smokeEffectInstantiate = Instantiate(smokeEffect, transform.position, transform.rotation) as GameObject;

            Destroy(smokeEffectInstantiate, 20f);
        }*/
    }
}