using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
namespace Player
{
    public class Timtry : Healer
    {


        [Header("Timtry")]
        public bool hasInstantiateAMine = false;
        public bool hasInstantiateAMineSLOW = false;

        public GameObject mine;

        public GameObject mineSLOW;
        public GameObject minePos;

        public GameObject canvasUI;

        public GameObject originshoot;


        public float mineStrengh;

        public bool isZooming = false;
        public float cameraFOV;
        public Camera cam;

        public Vector3 force;
        public string enemyColor;

        public float waitmarket = 1f;

        public float rangecap2; // range of the blessing of timtry
        public float durationhealing; // duration of the mark of healing
        public int puissanceshealing; // amount of life recovered by tick

        public float durationzoneofslowness; // look at end of file with sequence for more comprehension
        public float durationachievedslow = 0f;
        public bool zoneActivated = false; // if true the prefab slowzone is instanciated

        private void Start()
        {
            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;

            cam = GetComponentInChildren<Camera>();
            cameraFOV = cam.fieldOfView;

            canvasUI.GetComponent<UI>().hasShield = false;
            canvasUI.GetComponent<UI>().hasThreeCapacities = false;
            canvasUI.GetComponent<UI>().maxHP = hpmax;

            GetComponent<SlowdeZone>().CreateEnnemiTeam(GetComponent<TeamColor>().teamColor);

            GetComponent<HealDazze>().ovetimeheal = puissanceshealing;
            GetComponent<HealDazze>().timeofheal = durationhealing;
        }
        void FixedUpdate()
        {

            if (hp <= 0)
            {
                //Debug.Log("Dead");
                //dead();
            }

            canvasUI.GetComponent<UI>().CurrentHP = hp;

            cam.fieldOfView = cameraFOV;


            if (durationachievedslow == 1f)
            {
                Destroy(GameObject.FindGameObjectWithTag("slowzone"));
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
                if (zoneActivated)
                {
                    GetComponent<SlowdeZone>().IsInContact();
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
            Debug.Log("run");
            GetComponentInChildren<Weapon.WeaponSniper>().Shoot();

        }
        private void M2()
        {
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

        private void Cap2()
        {
            if (canvasUI.GetComponent<UI>().percentageCooldown2 == 1)
            {
                canvasUI.GetComponent<UI>().cap("two");

                GameObject clone = Instantiate(mineSLOW, minePos.transform.position, Quaternion.identity) as GameObject; //minePos.transform.rotation
                clone.SendMessage("SetColor", this.GetComponent<TeamColor>().enemieColor);
                clone.SendMessage("DEBUG", DEBUG);

                Vector3 force = transform.forward;
                force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * cam.transform.rotation.eulerAngles.x) * 2f, force.z);
                force *= mineStrengh;
                clone.GetComponent<Rigidbody>().AddForce(force);

                hasInstantiateAMineSLOW = true;
            }
        }

        private void Ulti()
        {
            //double les dmg
        }



        private void dead()
        {
            gameObject.GetComponent<Moving>().enabled = false;
            this.enabled = false;
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
        Sequence wait()
        {
            waitmarket = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => waitmarket, x => waitmarket = x, 1, 0.25f));
            return s;
        }
        Sequence durationzoneslow()
        {
            durationachievedslow = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => durationachievedslow, x => durationachievedslow = x, 1, durationhealing));
            return s;
        }


    }



}