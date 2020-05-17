using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Player
{
    public class Roy : Tank
    {
        [Header("Roy")]
        public bool hasInstantiateAMine = false;

        public GameObject canvasUI;
        public Quaternion rotation;

        public GameObject mine;
        public GameObject minePos;

        public float mineStrengh;
        public float cameraFOV;
        public Camera cam;
        private float time = 15f;
        public int power = 15;

        [Header("Grappin")]
        public GameObject grap;

        public float actualtime = 0f;
        public float cdtime = 3;

        public float waitmarket = 1f;

        void Start()
        {
            GetComponent<Moving>().speed = speed;
            GetComponent<Moving>().jumpspeed = jumpspeed;


            cam = GetComponentInChildren<Camera>();
            cameraFOV = cam.fieldOfView;

            canvasUI.GetComponent<UI>().hasShield = false;
            canvasUI.GetComponent<UI>().hasThreeCapacities = true;
            canvasUI.GetComponent<UI>().maxHP = hpmax;
            shieldLife = 100;
            shieldRecovery = 10;




        }

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
                    Cap3();
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
        private void FixedUpdate()
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
                GetComponent<Moving>().jumpspeed = jumpspeed;
            }
            else
            {
                timer();
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

        private void M1()
        {
            this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
        }

        private void Cap3()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown3 == 1)
            {
                canvasUI.GetComponent<UI>().cap("three");

                Vector3 rot = new Vector3(180, 180, 0);

                // player : y et w
                // cam

                Vector3 init = transform.rotation.eulerAngles;

                rot.y += init.y;

                GameObject grappin = Instantiate(grap, minePos.transform.position, Quaternion.Euler(rot)) as GameObject;
                Vector3 force = transform.forward;
                force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * cam.transform.rotation.eulerAngles.x) * 2f, force.z);
                force *= power;

                grappin.GetComponent<Rigidbody>().AddForce(force);
                grappin.GetComponent<Grappin>().pos = transform.position;
            }
        }

        private void Cap1()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                canvasUI.GetComponent<UI>().cap("one");
                GameObject clone = Instantiate(mine, minePos.transform.position, Quaternion.identity) as GameObject; //minePos.transform.rotation
                clone.SendMessage("SetColor", this.GetComponent<TeamColor>().enemieColor);
                clone.SendMessage("DEBUG", DEBUG);

                Vector3 force = transform.forward;
                force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * cam.transform.rotation.eulerAngles.x) * 2f, force.z);
                force *= mineStrengh;
                clone.GetComponent<Rigidbody>().AddForce(force);

                hasInstantiateAMine = true;
            }
        }

        private void Cap2() // speed boost for all allies in radius
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                actualtime = cdtime;
                canvasUI.GetComponent<UI>().cap("two");
                this.GetComponent<Moving>().jumpspeed = jumpspeed * 2f;
            }
        }
        public void timer()
        {
            if (actualtime > 0)
            {
                actualtime -= Time.deltaTime;
            }
        }

        private void Ulti()
        {
            canvasUI.GetComponent<UI>().cap("ulti");
            float hpSave = canvasUI.GetComponent<UI>().CurrentHP;
            float maxHpSave = canvasUI.GetComponent<UI>().maxHP;
            canvasUI.GetComponent<UI>().maxHP = 1000f;
            canvasUI.GetComponent<UI>().CurrentHP = 1000f;
            float percentageCooldown1 = 0;
            Sequence s = DOTween.Sequence();
            canvasUI.GetComponent<UI>().maxHP = maxHpSave;
            canvasUI.GetComponent<UI>().CurrentHP = hpSave;
        }
        public void ApllyCard(Card upgrade)
        {
            hpmax += (int)upgrade.maxhp;
            canvasUI.GetComponent<UI>().maxHP += hpmax;
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