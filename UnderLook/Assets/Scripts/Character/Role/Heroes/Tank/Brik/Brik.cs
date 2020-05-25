using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

namespace Player
{
    public class Brik : Tank
    {
        [Header("Brick")]
        public bool hasInstantiateAMine = false;

        public GameObject canvasUI;

        public GameObject Shield;
        public Vector3 cam1, cam2;
        public GameObject camera;

        public GameObject mine;
        public float mineStrengh;
        public GameObject minePos;

        public GameObject skin;

        public float waitmarket = 1f;
        public float timeofmine = 6;

        private void Start()
        {

            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;
            GetComponent<Moving>().originalSpeed = speed;

            canvasUI.GetComponent<UI>().hasShield = true;
            canvasUI.GetComponent<UI>().hasThreeCapacities = false;
            canvasUI.GetComponent<UI>().maxHP = hpmax;
            canvasUI.GetComponent<UI>().maxShield = shieldLifeMax;
        }
        void FixedUpdate()
        {
            GetComponent<shieldState>().state = shield;
            canvasUI.GetComponent<UI>().CurrentShield = shieldLife;

            Shield.transform.localRotation = new Quaternion(0, 0, 0, 1);

            if (!shield && shieldLife < shieldLifeMax)
            {
                shieldLife += shieldRecovery * Time.deltaTime;
                if (shieldLife > shieldLifeMax)
                {
                    shieldLife = shieldLifeMax;
                }
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

        //-----------------------------
        private void Update()
        {
            if (!canvasUI.GetComponent<UI>().showmenu && !canvasUI.GetComponent<UI>().showmarket)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift)) //shield
                {
                    Cap1();
                }
                if (Input.GetKeyDown(KeyCode.E)) // grenade
                {
                    Cap2();
                }
            }

        }


        private void Cap1()
        {
            //shield
            Shield.SetActive(!Shield.activeSelf);
            shield = !shield;
            skin.SetActive(shield);
            Vector3 pos = new Vector3(0,0,0);
            pos = shield ? cam2 : cam1;
            DOTween.Play(Moving(pos));
            
        }

        private void Cap2()
        {
            //immobilisation
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");

                hasInstantiateAMine = true;

                GameObject clone = Instantiate(mine, minePos.transform.position, Quaternion.identity) as GameObject;

                clone.SendMessage("SetColor", this.GetComponent<TeamColor>().enemieColor);
                clone.SendMessage("DEBUG", DEBUG);

                Vector3 force = transform.forward;
                force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * camera.transform.rotation.eulerAngles.x) * 2f, force.z);
                force *= mineStrengh;
                clone.GetComponent<Rigidbody>().AddForce(force);
            }
        }


        Sequence Moving(Vector3 pos)
        {
            Sequence s = DOTween.Sequence();
            s.Append(camera.transform.DOLocalMove(pos, 0.3f));//.SetEase(Ease.InBounce)) ;
            return s;
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
            GetComponent<Moving>().speed *= (1 + upgrade.speed / 100);
            shieldLifeMax *= (1 + upgrade.maxshield / 100);
            canvasUI.GetComponent<UI>().maxShield = shieldLifeMax;
            timeofmine += upgrade.durationCap1;


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