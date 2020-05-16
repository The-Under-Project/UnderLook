using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Win32;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    public Quaternion rotation;
    public int gunDamage;//Damage of weapon , for the moment it's fixed but we need to redefine it
    public float fireRate;//Rate of weapon  
    //public Transform gunPosition;//Position of gun to calculate the line
    public Transform shootPoint;
    public Camera fpscam;

    public int power; //vitesse d'ejection
    public float time; //temps avant suppression de balle
    public GameObject bullet; //Balle utilis�e
    public GameObject player;

    //private AudioSource gunAudio; //The audio of gun
    private float nextFire; // Time when you can do a new shot
                            // Start is called before the first frame update

    // Update is called once per frame
    public void Shoot()
    {
        if (Time.time > nextFire)  //je ne pense pas que �a marche
        {
            nextFire = Time.time + fireRate;
            Quaternion rot = rotation;
            rot.x += fpscam.transform.rotation.x;
            rot.y += player.transform.rotation.y;
            rot.z += player.transform.rotation.z;
            rot.w += player.transform.rotation.w + fpscam.transform.rotation.w;
            GameObject shootedBullet = Instantiate(bullet, shootPoint.position, rotation) as GameObject;
            Vector3 force = player.transform.forward;
            force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * fpscam.transform.rotation.eulerAngles.x) * 2f, force.z);
            force *= power;
            shootedBullet.GetComponent<Rigidbody>().AddForce(force);

            Destroy(shootedBullet, time);

            void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.CompareTag("Floor"))
                {
                    Destroy(shootedBullet, time);
                }
            }
            Destroy(shootedBullet, time);
        }

    }
}