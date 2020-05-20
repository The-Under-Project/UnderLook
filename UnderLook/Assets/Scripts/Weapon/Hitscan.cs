using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public abstract class Hitscan : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioClip shootAudio = null;
        private PhotonView _view;

        [Header("Settings")]
        
        public PhotonView photonView;
        public Transform pointShoot;
        public Transform pointOut;

        public int gunDamage;//Damage of weapon , for the moment it's fixed but we need to redefine it
        public float fireRate;//Rate of weapon  
        public float weaponRange;//Range of wepon
        public float hitForce; //Knockback of weapon

        public Transform gunPosition;//Position of gun to calculate the line

        public Camera fpscam;
        private WaitForSeconds shotDuration;//Duration of line

        //private AudioSource gunAudio; //The audio of gun
        [SerializeField] private LineRenderer gunLine; //The line of shoot 
        private float nextFire; // Time when you can do a new shot

        // Start is called before the first frame update
        void Start()
        {
            photonView =gameObject.GetComponentInParent<PhotonView>();
            shotDuration = new WaitForSeconds(fireRate/2);
            _view = GetComponent<PhotonView>();
            //gunLine = GetComponent<LineRenderer>();
            //gunAudio = GetComponent<AudioSource>();
            //fpscam = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        public void Shoot()
        {
            if (Time.time > nextFire)  //je ne pense pas que ça marche
            {
                playSoundLazer(_view.viewID);
                nextFire = Time.time + fireRate; //Set the time for another shot 
                StartCoroutine(ShotEffect());

                //Vector3 lineOrigin = fpscam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));//Define center
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                gunLine.SetPosition(0, gunPosition.position);

                if (Physics.Raycast(ray, out hit, weaponRange))
                {
                    gunLine.SetPosition(1, hit.point);
                    //pas générique

                    if (hit.transform.tag == "Player")
                    {
                        Vector2 send = new Vector2(hit.transform.GetComponent<PhotonView>().viewID, gunDamage);
                        hit.transform.GetComponent<PhotonView>().photonView.RPC("Dmg", PhotonTargets.All, send);

                        if (hit.transform.GetComponent<Player.Cassie>() != null && hit.transform.GetComponent<PlayerManagerCassie>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);
                        else if (hit.transform.GetComponent<Player.Brik>() != null && hit.transform.GetComponent<PlayerManagerBrik>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);
                        else if (hit.transform.GetComponent<Player.Yalee>() != null && hit.transform.GetComponent<PlayerManagerYalee>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);
                        else if (hit.transform.GetComponent<Player.Timtry>() != null && hit.transform.GetComponent<PlayerManagerTimtry>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);
                        else if (hit.transform.GetComponent<Player.Easwith>() != null && hit.transform.GetComponent<PlayerManagerEaswith>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);
                        else if (hit.transform.GetComponent<Player.Roy>() != null && hit.transform.GetComponent<PlayerManagerRoy>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);
                        else if (hit.transform.GetComponent<Player.Rasla>() != null && hit.transform.GetComponent<PlayerManagerRasla>().Health <= gunDamage)
                            hit.transform.GetComponent<Basics.GenerateTXT>().Add(4, GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName);



                        //photonView.RPC("Dmg", PhotonTargets.All, hit.transform.GetComponent<PhotonView>().viewID);
                    }

                }
                else
                {
                    gunLine.SetPosition(1, pointOut.transform.position);
                }

            }

        }
        [PunRPC]
        void playSoundLazer(int viewID)
        {
            GameObject player = PhotonView.Find(viewID).gameObject;
            player.GetComponent<AudioSource>().PlayOneShot(shootAudio);
        
            if (GetComponent<PhotonView>().isMine)
            {
                _view.RPC("playSoundLazer", PhotonTargets.OthersBuffered, viewID);
            }
        }

        private IEnumerator ShotEffect() // Execute effect: Line + Audio
        {
            //gunAudio.Play();
            gunLine.enabled = true;
            yield return shotDuration;
            gunLine.enabled = false;
        }

        public int setGunDamage
        {
            set { gunDamage = value; }
        }
        public float setFireRate
        {
            set { fireRate = value; }
        }
        public float setWeaponRange
        {
            set { weaponRange = value; }
        }
        public float setHitForce
        {
            set { hitForce = value; }
        }
    }
    
}