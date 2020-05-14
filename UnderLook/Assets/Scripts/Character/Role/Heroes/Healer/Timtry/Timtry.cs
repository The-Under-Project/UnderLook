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

        public GameObject canvasUI;

        //public GameObject mine;
        public GameObject originshoot;
        

        public float mineStrengh;

        public bool isZooming = false;
        public float cameraFOV;
        public Camera cam;

        /*public bool isShadow = false;
        public GameObject sphere;

        public float teleportRange;

        public float Teleport = 100;
        */
        /*public float TimeTP;
        public float TimeAfterTP;
        public float percentage;
        */

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

            
            if(durationachievedslow == 1f)
            {
                Destroy(GameObject.FindGameObjectWithTag("slowzone"));
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

        }


        private void M1()
        {
            GetComponentInChildren<Weapon.WeaponAssaultRifle>().Shoot();

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

            if (canvasUI.GetComponent<UI>().percentageCooldown1 == 1f)
            {
                Destroy(GameObject.FindGameObjectWithTag("slowzone"));
                canvasUI.GetComponent<UI>().cap("one");
                GetComponent<SlowdeZone>().slowzone();
                zoneActivated = true;
                durationzoneslow();
                
            }
        }

        private void Cap2()
        {
            if(canvasUI.GetComponent<UI>().percentageCooldown2 == 1f)
            {
                canvasUI.GetComponent<UI>().cap("two");
                GetComponent<HealDazze>().DeterminedHealed(originshoot.transform, GetComponent<TeamColor>().teamColor, rangecap2);

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
        Sequence durationzoneslow()
        {
            durationachievedslow = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() =>durationachievedslow, x => durationachievedslow = x, 1, durationhealing));
            return s;
        }


    }



}
