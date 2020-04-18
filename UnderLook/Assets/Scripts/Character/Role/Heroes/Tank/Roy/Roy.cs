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
        [Header("Roy")] public GameObject canvasUI;
        public float cameraFOV;
        public Camera cam;
        public bool hasInstantiateAShield = false;
        private float time = 15f;
        private int power = 15;
        public GameObject shield;

        [Header("Grappin")] public GameObject grap;
        public float timergrap;
        public float cooldownM2;
        public Collision collision;
        public float forceofGrap;
        public bool inMotion;
        public Boolean cd = false;
        private float time1 = 20f;
        public GameObject grapPose;

        void Start()
        {
            GetComponent<Moving>().speed = speed;
            GetComponent<Moving>().jumpspeed = jumpspeed;


            cam = GetComponentInChildren<Camera>();
            cameraFOV = cam.fieldOfView;

            canvasUI.GetComponent<UI>().hasShield = true;
            canvasUI.GetComponent<UI>().hasThreeCapacities = false;
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

            if (Input.GetKeyDown(KeyCode.A))
            {
                Ulti();
            }

            Debug.Log(canvasUI.GetComponent<UI>().CurrentHP);
            Debug.Log(canvasUI.GetComponent<UI>().maxHP);
        }

        private void M1()
        {
            this.GetComponentInChildren<Weapon.WeaponCannon>().Shoot();
        }

        private void M2()
        {
            if (!inMotion)
            {
                inMotion = true;
                GameObject grappin = Instantiate(grap, grapPose.transform.position, Quaternion.identity) as GameObject;
                collision = grappin.GetComponent<Collision>();
                grappin.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * forceofGrap);
                if (collision.gameObject.CompareTag("Player"))
                {
                    RetourGrappin(collision.gameObject);

                }
            }

        }

        private void Cap1()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                canvasUI.GetComponent<UI>().cap("one");
                GameObject shootedShield = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;
                shootedShield.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
                //shootedShield.SendMessage("SetColor", GetComponent<TeamColor>().teamColor);
                Destroy(shootedShield, time);
            }
        }

        private void Cap2()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");
                GetComponentInChildren<Moving>().capacity = true;
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
            Boolean cdRefresh1 = true;
            Sequence s = DOTween.Sequence();

            if (!cd)
                s.Append(DOTween.To(() => percentageCooldown1, x => percentageCooldown1 = x, 1, time1));
            else
                percentageCooldown1 = 1;
            canvasUI.GetComponent<UI>().maxHP = maxHpSave;
            canvasUI.GetComponent<UI>().CurrentHP = hpSave;
        }



        public void RetourGrappin(GameObject foe)
        {
            foe.GetComponentInChildren<Moving>().canMove = false;
            foe.GetComponent<Rigidbody>().velocity = this.transform.TransformDirection(-Vector3.forward * forceofGrap);
        }


    }
}
