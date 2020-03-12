using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public abstract class Hitscan : MonoBehaviour
    {

        public Transform pointShoot;

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
            shotDuration = new WaitForSeconds(fireRate/2);
        }

        // Update is called once per frame
        public void Shoot()
        {
            if (Time.time > nextFire)  //je ne pense pas que ça marche
            {
                nextFire = Time.time + fireRate; //Set the time for another shot 
                StartCoroutine(ShotEffect());

                //Vector3 lineOrigin = fpscam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));//Define center
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                gunLine.SetPosition(0, gunPosition.position);

                if (Physics.Raycast(ray, out hit, weaponRange))
                {
                    gunLine.SetPosition(1, hit.point);
                    //Here We need the system of box life -> so I can't continue here
                }
                if (Physics.Raycast(ray, out hit,  Mathf.Infinity))
                {
                    gunLine.SetPosition(1, hit.point);
                    //Here We need the system of box life -> so I can't continue here
                }

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