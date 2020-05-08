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
            if (isShadow && !GetComponentInChildren<TPoverlapCircle>().colAbove && canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
            {
                
                if(use > 0)
                {
                    use--;
                    started = true;
                    tp();
                }
            }

        }
        private void M2()
        {

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

    }

}