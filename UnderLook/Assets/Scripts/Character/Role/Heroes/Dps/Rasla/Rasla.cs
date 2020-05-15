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
        public Camera cameraofperso;
        public int oldwask;
        public float waitmarket = 1f;
        public float powergrenada;
        public float cdgrenade;
        public float tempsgre;
        public float tempsrestatngre;
        public GameObject[] debug;

        private void Start()
        {


            this.GetComponent<Moving>().speed = speed;
            this.GetComponent<Moving>().jumpspeed = jumpspeed;

            canvasUI.GetComponent<UI>().hasShield = false;
            canvasUI.GetComponent<UI>().hasThreeCapacities = false;
            canvasUI.GetComponent<UI>().maxHP = hpmax;

            string allyteam = GetComponent<TeamColor>().teamColor;
            debug = GameObject.FindGameObjectsWithTag("Player");
            foreach (var machin in debug)
            {
                if (machin.GetComponent<TeamColor>().enemieColor != allyteam)
                {
                    machin.GetComponentInChildren<WeaponRotateCamera>().gameObject.layer = 10;
                    machin.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = 10;
                    machin.layer = 10;
                }
            }
            canvasUI.GetComponent<UI>().time3 = cdgrenade;


        }
        void FixedUpdate()
        {
            if (invicible)
                hp = hpbeforecap2;

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
            if (timedepbegininvi == 1)
            {
                invicible = false;
                cameraofperso.cullingMask = oldwask;

            }
            if(tempsrestatngre == 1)
            {
                Object.Destroy(GameObject.FindGameObjectWithTag("grenade"));
                GetComponent<effetBlur>().launchedgrenada = false;
                GetComponent<effetBlur>().Deactivate();
                
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
                this.GetComponentInChildren<Weapon.WeaponAssaultRifle>().Shoot();
            }
            if(isShadow && !GetComponentInChildren<TPoverlapCircle>().colAbove && canvasUI.GetComponent<UI>().percentageCooldown1 == 1)
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
            
            if( canvasUI.GetComponent<UI>().percentageCooldown3 == 1)
            {
                Debug.Log("allo");
                canvasUI.GetComponent<UI>().cap("three");
                GetComponent<effetBlur>().GenerateEnnemiTeam(GetComponent<TeamColor>().teamColor);
                GetComponent<effetBlur>().LaunchedGrenada(this.gameObject, cameraofperso, powergrenada);
                GrenadaDuration();
                
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
                hpbeforecap2 = hp;
                invicible = true;
                Invicibilityseq();
                oldwask = cameraofperso.cullingMask;
                cameraofperso.cullingMask = cameraofperso.cullingMask ^ (1 << 10);

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
        Sequence GrenadaDuration()
        {
            tempsrestatngre = 0;
            Sequence s = DOTween.Sequence();
            s.Append(DOTween.To(() => tempsrestatngre, x => tempsrestatngre = x, 1f, tempsgre));
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