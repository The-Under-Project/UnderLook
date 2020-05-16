using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class Rasla : Dps
    {
        [Header("Rasla")]
        public GameObject canvasUI;

        public bool hasInstantiateAMine = false;

        [Header("TP")]
        public GameObject sphere;
        public float teleportRange;
        public float Teleport = 100;
        public bool isShadow = false;
        public float TimeTP;
        public float TimeAfterTP;
        public float percentage;
        public bool started = false;
        public int useMax = 3;
        public int use = 3;


        [Header("PasTp")]
        private int hpbeforecap2;
        public bool invicible = false;
        public float timeofinvincibility;
        public float timedepbegininvi = 0;
        public Camera cam;

        public GameObject mine;
        public GameObject minePos;

        public float mineStrengh;

        public int oldwask;

        public int oldHPmax;

        public float actualtime = 0f;
        public float timeMax = 0f;

        private void Start()
        {

            oldHPmax = hpmax;
            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;

            canvasUI.GetComponent<UI>().hasShield = false;
            canvasUI.GetComponent<UI>().hasThreeCapacities = true;
            canvasUI.GetComponent<UI>().maxHP = hpmax;

            string allyteam = GetComponent<TeamColor>().teamColor;


        }
        void FixedUpdate()
        {
            timer();
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
                hpmax = oldHPmax;
            }

            canvasUI.GetComponent<UI>().CurrentHP = hp;


            if (isShadow) //si jamais tu es dans la range et que les bords de la sphère  sont bien en contact 'trigger' 
                          //alors tu peux te tp. Si jamais il y  aps de hit, tu regarde à la distance max si jamais tu peux te tp
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, teleportRange))
                {

                    sphere.transform.position = hit.point;
                }
            }
            if (use <= 0)
            {
                use = useMax;
                sphere.SetActive(false);
                isShadow = false;
                canvasUI.GetComponent<UI>().cap("one");
                started = false;

            }
            if (timedepbegininvi == 1)
            {
                invicible = false;
                cam.cullingMask = oldwask;

            }
            /*
            if (tempsrestatngre == 1)
            {
                Object.Destroy(GameObject.FindGameObjectWithTag("grenade"));
                GetComponent<effetBlur>().launchedgrenada = false;
                GetComponent<effetBlur>().Deactivate();

            }*/
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
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Cap1();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Cap2();
                }

        }


        private void M1()
        {
            if (!isShadow)
            {
                this.GetComponentInChildren<Weapon.WeaponSniper>().Shoot();
            }
            if (isShadow && !GetComponentInChildren<TPoverlapCircle>().colAbove && canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                if (use > 0)
                {
                    use--;
                    started = true;
                    tp();
                }
            }

        }
        private void M2()
        {

            if (canvasUI.GetComponent<UI>().percentageCooldown3 == 1)
            {
                canvasUI.GetComponent<UI>().cap("three");
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

        private void Cap1()
        {
            if (isShadow && started)
            {
                sphere.SetActive(false);
                isShadow = false;
            }
            else if (!isShadow && canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                sphere.SetActive(true);
                isShadow = true;
            }
        }

        private void Cap2()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");
                hpmax = 99999999;
                hp = hpmax;
                actualtime = timeMax;

            }
        }

        private void tp()
        {
            DOTween.Play(Teleportseq());
        }

        Sequence Teleportseq()
        {
            Teleport = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => Teleport, x => Teleport = x, percentage, TimeTP));
            s.Append(transform.DOLocalMove(sphere.transform.position + new Vector3(0, 0.5f), 0.05f)); //not tp into floor
            return s;
        }
        Sequence Invicibilityseq()
        {
            timedepbegininvi = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => timedepbegininvi, x => timedepbegininvi = x, 1f, timeofinvincibility));
            return s;
        }
        public void timer()
        {
            if (actualtime > 0)
            {
                actualtime -= Time.deltaTime;
            }
        }
    }

}