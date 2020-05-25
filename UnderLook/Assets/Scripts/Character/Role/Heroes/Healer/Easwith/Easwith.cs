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

        public float waitmarket = 1f;
        public float jumpspeedup = 1f;

        private void Start()
        {
            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;
            GetComponent<Moving>().originalSpeed = speed;

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
                this.GetComponent<Moving>().jumpspeed = jumpspeed * 2f * jumpspeedup;
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
        public void ApllyCard(Card upgrade)
        {
            hpmax += (int)upgrade.maxhp;
            canvasUI.GetComponent<UI>().maxHP = hpmax;
            hp += (int)upgrade.maxhp;
            canvasUI.GetComponent<UI>().CurrentHP = hp;
            // canvasUI.GetComponent<UI>().maxShield *= (1 + upgrade.maxshield / 100); maxshield private en UI mais pas maxHP?

            canvasUI.GetComponentInChildren<UI>().time1 *= (1 - upgrade.coolDownCap1 / 100);
            canvasUI.GetComponent<UI>().time2 *= (1 - upgrade.coolDownCap2 / 100);
            canvasUI.GetComponent<UI>().time3 *= (1 - upgrade.coolDownCap3 / 100);

            GetComponent<Moving>().jumpspeed *= (1 + (upgrade.jumpspeed / 100));
            GetComponent<Moving>().gravity *= (1 - upgrade.gravity / 100);
            speed =(int)(speed *  (1 + upgrade.speed / 100) + 1 );

            jumpspeedup *= (1 + upgrade.damageCap1 / 100);
            weaponHEAL.GetComponent<Weapon.WeaponSniper>().gunDamage =(int)(weaponHEAL.GetComponent<Weapon.WeaponSniper>().gunDamage*(1 + upgrade.damageCap2 / 100));


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