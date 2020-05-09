using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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



        private void Start()
        {

            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;

            canvasUI.GetComponent<UI>().hasShield = true;
            canvasUI.GetComponent<UI>().hasThreeCapacities = false;
            canvasUI.GetComponent<UI>().maxHP = hpmax;
            canvasUI.GetComponent<UI>().maxShield = shieldLifeMax;
        }
        void FixedUpdate()
        {

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
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Cap1();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cap2();
            }

        }


        private void Cap1()
        {
            //shield
            Shield.SetActive(!Shield.activeSelf);
            shield = !shield;
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

    }
}