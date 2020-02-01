using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
namespace Player
{
    public class Cassie : Dps
    {
        public GameObject mine;
        public GameObject minePos;

        public float mineStrengh;

        public bool isZooming = false;
        public float cameraFOV;
        public Camera cam;

        public bool isShadow = false;
        public GameObject sphere;

        public float teleportRange;

        public float Teleport = 100;

        public float TimeTP;
        public float TimeAfterTP;
        public float percentage;

        public Cassie(int hp, int speed, int jump) : base(hp, speed, jump) //heritage
        {

        }




        private void Start()
        {
            cam = GetComponentInChildren<Camera>();
            cameraFOV = cam.fieldOfView;

            percentage = TimeTP / (TimeTP + TimeAfterTP) * 100;

        }
        void FixedUpdate()
        {

            cam.fieldOfView = cameraFOV;

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


            if (Teleport < percentage)
            {
                GetComponent<Moving>().gravityApplied = false;
                GetComponent<Moving>().canMove = false;
            }
            else
            {
                if (Teleport >= 100)
                {
                    GetComponent<Moving>().canMove = true;
                }
                else
                    GetComponent<Moving>().canMove = false;

                GetComponent<Moving>().gravityApplied = true;
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                Ulti();
            }

        }


        private void M1()
        {
            if (isShadow && !GetComponentInChildren<TPoverlapCircle>().colAbove)
            {
                if (GetComponentInChildren<TPoverlapCircle>().nbCol == 4)
                {
                    tp(true);
                }
                else if (GetComponentInChildren<TPoverlapCircle>().nbCol == 5)
                {
                    tp(false);
                }
            }
            else
            {
                this.GetComponentInChildren<Weapon.CassieWeapon>().Shoot();
                //hitscan shot
            }
        }
        private void M2()
        {
            if (cam.fieldOfView == 60)
            {
                DOTween.Play(ZoomIN());
                isZooming = true;
            }
            else if (cam.fieldOfView == 30)
            {
                DOTween.Play(ZoomOUT());
                isZooming = true;
            }
        }

        private void Cap1()
        {
            Debug.Log("Change");
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

            GameObject clone = Instantiate(mine, minePos.transform.position, minePos.transform.rotation) as GameObject;

            clone.SendMessage("SetColor", this.GetComponent<TeamColor>().enemieColor);

            Vector3 force = transform.forward;

            force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * cam.transform.rotation.eulerAngles.x) * 2f, force.z);

            force *= mineStrengh;


            clone.GetComponent<Rigidbody>().AddForce(force);

        }

        private void Ulti()
        {
            //double les dmg
        }

        private void tp(bool offset)
        {
            isShadow = false;

            sphere.SetActive(false);

            DOTween.Play(Teleportseq(offset));
        }



        Sequence ZoomIN()
        {
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => cameraFOV, x => cameraFOV = x, 30f, 0.2f));
            return s;
        }
        Sequence ZoomOUT()
        {
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => cameraFOV, x => cameraFOV = x, 60f, 0.2f));
            return s;
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
                switch (e) //switch the value that is false and tp with offset toward the opposite circle 
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
    }
}
