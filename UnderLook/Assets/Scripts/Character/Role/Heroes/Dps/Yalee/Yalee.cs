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

        public float waitmarket = 1f;

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
            if (hp > hpmax)
            {
                hp = hpmax;
            }

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
            if (Input.GetKey(KeyCode.F1) && !GetComponentInChildren<Market>().itembought && !canvasUI.GetComponent<UI>().showmenu && waitmarket == 1f)
            {
                wait();
                if (canvasUI.GetComponent<UI>().showmarket)
                    canvasUI.GetComponent<UI>().showmarket = false;
                else
                {
                    canvasUI.GetComponent<UI>().showmarket = true;
                }
            }
            if (GetComponentInChildren<Market>().itembought && canvasUI.GetComponent<UI>().showmarket)
            {
                ApllyCard(GetComponentInChildren<Market>().item);
                canvasUI.GetComponent<UI>().showmarket = false;
            }


            if (Input.GetKey(KeyCode.Escape) && !canvasUI.GetComponent<UI>().showmenu && !canvasUI.GetComponent<UI>().showmarket)
            {
                canvasUI.GetComponent<UI>().showmenu = true;
                Cursor.lockState = CursorLockMode.Confined;
                GetComponentInChildren<CameraController>().canmovevision = false;
            }
            if (Input.GetKey(KeyCode.Escape) && canvasUI.GetComponent<UI>().showstat)
            {
                canvasUI.GetComponent<UI>().stat.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Escape) && canvasUI.GetComponent<UI>().showoption)
                canvasUI.GetComponent<UI>().option.SetActive(false);

        }

        //-----------------------------
        private void Update()
        {
            if (!canvasUI.GetComponent<UI>().showmenu && !canvasUI.GetComponent<UI>().showmarket)
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

        }


        private void M1()
        {
            this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
        }
        private void M2()
        {

            this.GetComponentInChildren<Weapon.WeaponDagger>().TripleShoot();
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
        public void ApllyCard(Card upgrade)
        {
            hpmax = (int)upgrade.maxhp;
            canvasUI.GetComponent<UI>().maxHP = hpmax;
            // canvasUI.GetComponent<UI>().maxShield *= (1 + upgrade.maxshield / 100); maxshield private en UI mais pas maxHP?

            canvasUI.GetComponentInChildren<UI>().time1 *= (1 - upgrade.coolDownCap1 / 100);
            canvasUI.GetComponent<UI>().time2 *= (1 - upgrade.coolDownCap2 / 100);
            canvasUI.GetComponent<UI>().time3 *= (1 - upgrade.coolDownCap3 / 100);

            GetComponent<Moving>().jumpspeed *= (1 + (upgrade.jumpspeed / 100));
            GetComponent<Moving>().gravity *= (1 - upgrade.gravity / 100);
            GetComponent<Moving>().speed *= (1 + upgrade.speed / 100);
            GetComponentInChildren<Weapon.WeaponDagger>().gunDamage = (int)(GetComponentInChildren<Weapon.WeaponDagger>().gunDamage * (1 + upgrade.ultimateDamge / 100));


        }
        Sequence wait()
        {
            waitmarket = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => waitmarket, x => waitmarket = x, 1, 0.25f));
            return s;
        }
    }
}