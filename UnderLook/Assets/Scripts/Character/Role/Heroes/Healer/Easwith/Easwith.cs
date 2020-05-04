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
            if (Input.GetKeyDown(KeyCode.LeftShift)) // speed boost
            {
                Cap1();
            }
            if (Input.GetKeyDown(KeyCode.E)) // heal mark
            {
                Cap2();
            }

        }


        private void M1() // assault rifle
        {
            //this.GetComponentInChildren<Weapon.>().Shoot(); // projectile weapn
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

                float current_position = transform.position;
                players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject p in players)
                {
                    if (Vector3.Distance(p.position,current_position) < 20f) // players in range
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

                float current_position = transform.position;
                players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject p in players)
                {
                    if (Vector3.Distance(p.position,current_position) < 10f) // players in range
                    {
                        StartCoroutine(HealEffect(p));
                    }
                }
            }
        }

        IEnumerator SpeedEffect(GameObject p)
        {
            p.GetComponent<Moving>().speed *= 1.2; // 20% ms increase

            yield return new WaitForSeconds(5); // duration

            p.speed = p.GetComponent<Moving>().speed/1.2;
        }

        IEnumerator HealEffect(GameObject p)
        {
            p.hp += 20;
            yield return new WaitForSeconds(0.5);
            p.hp += 20;
            yield return new WaitForSeconds(0.5);
            p.hp += 20;
            yield return new WaitForSeconds(0.5);
            p.hp += 20;
            yield return new WaitForSeconds(0.5);
            p.hp += 20;
        }
    }
}
