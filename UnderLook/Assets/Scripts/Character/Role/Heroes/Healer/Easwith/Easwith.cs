using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class Easwith: Healer
    {
        [Header("Easwith")]
        public GameObject canvasUI;

        public float waitmarket = 1f;

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
            this.GetComponentInChildren<Weapon.WeaponAssaultRifle>().Shoot();
        }
        private void M2()
        {
            // ?
        }

        private void Cap1() // speed boost for all allies in radius
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                canvasUI.GetComponent<UI>().cap("one");

                Vector3 current_position = transform.position;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject p in players)
                {
                    if (Vector3.Distance(p.transform.position,current_position) < 20f && p.GetComponent<TeamColor>().teamColor == this.GetComponent<TeamColor>().teamColor) // players in range
                    {
                        StartCoroutine(SpeedEffect(p));
                    }
                }
            }
        }

        private void Cap2() // heal mark
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");

                Vector3 current_position = transform.position;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject p in players)
                {
                    if (Vector3.Distance(p.transform.position,current_position) < 10f && p.GetComponent<TeamColor>().teamColor == this.GetComponent<TeamColor>().teamColor) // players in range
                    {
                        StartCoroutine(HealEffect(p));
                    }
                }
            }
        }

        IEnumerator SpeedEffect(GameObject p)
        {
            p.GetComponent<Moving>().speed *= 1.2f; // 20% ms increase

            yield return new WaitForSeconds(5); // duration

            speed = (int)(p.GetComponent<Moving>().speed/1.2f);
        }

        IEnumerator HealEffect(GameObject p)
        {
            int TotalHeal = 100;
            int Totalduration = 4;
            int steps = 100;
            for(int i = 0; i < steps; i++)
            {
                hp += TotalHeal/steps;
                yield return new WaitForSeconds(Totalduration/steps);
            }
        }

        public void ApllyCard(Card upgrade)
        {

            canvasUI.GetComponent<UI>().maxHP *= (1 + upgrade.maxhp / 100);
            // canvasUI.GetComponent<UI>().maxShield *= (1 + upgrade.maxshield / 100); maxshield private en UI mais pas maxHP?

            canvasUI.GetComponentInChildren<UI>().time1 *= (1 - upgrade.coolDownCap1 / 100);
            canvasUI.GetComponent<UI>().time2 *= (1 - upgrade.coolDownCap2 / 100);
            canvasUI.GetComponent<UI>().time3 *= (1 - upgrade.coolDownCap3 / 100);

            GetComponent<Moving>().jumpspeed *= (1 + (upgrade.jumpspeed / 100));
            GetComponent<Moving>().gravity *= (1 - upgrade.gravity / 100);
            GetComponent<Moving>().speed *= (1 + upgrade.speed / 100);



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
