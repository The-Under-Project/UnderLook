using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace Player
{
    public class Easwith : Healer
    {
        [Header("Easwith")]
        public GameObject canvasUI;

        public float actualtime = 0f;
        public float cdtime = 3;

        public GameObject weaponDMG;
        public GameObject weaponHEAL;

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
            time();
            if (hp > hpmax)
            {
                hp = hpmax;
            }
            if (hp <= 0)
            {
                //Debug.Log("Dead");
                //dead();
            }
            if (actualtime <= 0)
            {
                GetComponent<Moving>().speed = speed;
                GetComponent<Moving>().jumpspeed = jumpspeed;
            }

            canvasUI.GetComponent<UI>().CurrentHP = hp;
        }

        //-----------------------------
        private void Update()
        {
            if (true)//(!canvasUI.GetComponent<UI>().showmenu && !canvasUI.GetComponent<UI>().showmarket)
            {

                if (Input.GetButtonDown("Fire1"))
                {
                    M1();
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    M2();
                }
                if (Input.GetKeyDown(KeyCode.LeftShift)) // speed boost
                {
                    Cap1();
                }
                if (Input.GetKeyDown(KeyCode.E)) // heal mark
                {
                    Cap2();
                }
            }

        }


        private void M1() // assault rifle
        {
            weaponDMG.GetComponentInChildren<Weapon.WeaponSniper>().Shoot();
        }
        private void M2()
        {
            // ?
        }

        private void Cap1() // speed boost for all allies in radius
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                actualtime = cdtime;
                canvasUI.GetComponent<UI>().cap("one");
                this.GetComponent<Moving>().speed = speed * 2f;
                this.GetComponent<Moving>().jumpspeed = jumpspeed * 2f;
            }
        }

        private void Cap2() // heal mark
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");
                weaponHEAL.GetComponentInChildren<Weapon.WeaponSniper>().Shoot();
            }
        }
        public void time()
        {
            if (actualtime > 0)
            {
                actualtime -= Time.deltaTime;
            }
        }
    }
}