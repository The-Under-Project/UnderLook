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
    public class Yalee: Dps
    {
        [Header("NAME_OF_CHARACTER")]
        public GameObject canvasUI;

        private Boolean cd = false;
        public float time1 = 10f;

        private void Start()
        {


            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;

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
            this.GetComponentInChildren<Weapon.WeaponCannon>().Shoot();
        }
        private void M2()
        {
            //Nothing for this moment
        }

        private void Cap1()
        {
            this.GetComponent<Moving>().speed = speed*2;
            this.GetComponent<Moving>().jumpspeed = jumpspeed*2;
            
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
            this.GetComponent<UI>().CoolDown1 = true;
            this.GetComponent<UI>().CoolDown2 = true;
        }
    }
}