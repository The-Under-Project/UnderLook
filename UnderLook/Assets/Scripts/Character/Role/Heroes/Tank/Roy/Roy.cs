using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Player;
using UnityEngine;
using DG.Tweening;

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
        }

        private void M1()
        {
            this.GetComponentInChildren<Weapon.WeaponDagger>().Shoot();
        }

        private void Cap3()
        {
            canvasUI.GetComponent<UI>().cap("three");
            
            Quaternion rot = rotation;
            rot.x += cam.transform.rotation.x;
            rot.y += this.transform.rotation.y;
            rot.z += this.transform.rotation.z;
            rot.w += this.transform.rotation.w + cam.transform.rotation.w;
            
            GameObject grappin = Instantiate(grap, minePos.transform.position, Quaternion.identity) as GameObject;
            Vector3 force = transform.forward;
            force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * cam.transform.rotation.eulerAngles.x) * 2f, force.z);
            force *= power;

            grappin.GetComponent<Rigidbody>().AddForce(force);
            grappin.GetComponent<Grappin>().pos = transform.position;
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
    }
}