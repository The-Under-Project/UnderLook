using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class Tashe : Dps
    {
        [Header("Audio")]
        
        [SerializeField] private AudioClip runAudio = null;
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Tashe")]

        public GameObject canvasUI;
        public Camera cam;
        public GameObject shotPoint;




        public bool isShadow = false;
        public GameObject sphere;

        public float teleportRange;

        public float Teleport = 100;

        public float TimeTP;
        public float TimeAfterTP;
        public float percentage;



        public float actualtime = 0f;
        public float cdtime = 3;

        public float waitmarket = 1f;


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
            if (isShadow) //si jamais tu es dans la range et que les bords de la sphère  sont bien en contact 'trigger' 
                          //alors tu peux te tp. Si jamais il y  aps de hit, tu regarde à la distance max si jamais tu peux te tp
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Debug.Log("shadow");

                if (Physics.Raycast(ray, out hit, teleportRange))
                {
                    sphere.transform.position = hit.point;
                }
            }

            if (actualtime <= 0)
            {
                GetComponent<Moving>().jumpspeed = jumpspeed;
            }
            else
            {
                timer();
            }
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
        public void timer()
        {
            if (actualtime > 0)
            {
                actualtime -= Time.deltaTime;
            }
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
            if (!isShadow)
            {
                this.GetComponentInChildren<Weapon.WeaponRoquetteLauncher>().Shoot();
            }
            else if (isShadow && !GetComponentInChildren<TPoverlapCircle>().colAbove && canvasUI.GetComponent<UI>().percentageCooldown1 == 1 && GetComponentInChildren<TPoverlapCircle>().nbCol >= 4)
            {
                canvasUI.GetComponent<UI>().cap("one");
                if (GetComponentInChildren<TPoverlapCircle>().nbCol == 4)
                {
                    tp(true);
                }
                else if (GetComponentInChildren<TPoverlapCircle>().nbCol == 5)
                {
                    tp(false);
                }
            }

        }
        private void M2()
        {

        }

        private void Cap1()
        {
            if (isShadow)
            {
                sphere.SetActive(false);
                isShadow = false;
            }
            else
            {
                sphere.SetActive(true);
                isShadow = true;
            }
        }

        private void Cap2()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                actualtime = cdtime;
                canvasUI.GetComponent<UI>().cap("two");
                this.GetComponent<Moving>().jumpspeed = jumpspeed * 2f;
            }
        }
        private void tp(bool offset)
        {
            isShadow = false;

            sphere.SetActive(false);

            DOTween.Play(Teleportseq(offset));
        }
        Sequence Teleportseq(bool offset)
        {
            Vector3 offsetValue = new Vector3(0, 0, 0);

            if (offset)
            {
                int e = 0;
                bool[] check = sphere.GetComponent<TPoverlapCircle>().whoTrue;
                for (int i = 0; i < 5; i++)
                {
                    if (check[i] == false)
                    {
                        e = i;
                        break;
                    }
                }
                switch (e)
                {
                    case 0:
                        offsetValue = new Vector3(-2, 1);
                        break;
                    case 1:
                        offsetValue = new Vector3(2, 1);
                        break;
                    case 3:
                        offsetValue = new Vector3(0, 1, -2);
                        break;
                    case 4:
                        offsetValue = new Vector3(0, 1, 2);
                        break;
                }




            }
            Teleport = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => Teleport, x => Teleport = x, percentage, TimeTP));
            s.Append(transform.DOLocalMove(sphere.transform.position + new Vector3(0, 2) + offsetValue, 0.0001f)); //not tp into floor
            s.Append(DOTween.To(() => Teleport, x => Teleport = x, 100f, TimeTP));

            return s;
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