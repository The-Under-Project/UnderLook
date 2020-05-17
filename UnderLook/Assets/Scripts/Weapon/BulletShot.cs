using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Win32;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip shootAudio = null;
    [SerializeField] private AudioClip shootTripleAudio = null;
    private PhotonView _view;

    [Header("Settings")]
    public Quaternion rotation;
    public int gunDamage;//Damage of weapon , for the moment it's fixed but we need to redefine it
    public float fireRate;//Rate of weapon  
    //public Transform gunPosition;//Position of gun to calculate the line
    public Transform shootPoint;
    public Camera fpscam;

    public int power; //vitesse d'ejection
    public float time; //temps avant suppression de balle
    public GameObject bullet; //Balle utilisée
    public GameObject player;

    //private AudioSource gunAudio; //The audio of gun
    private float nextFire; // Time when you can do a new shot
                            // Start is called before the first frame update

    public void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

                            // Update is called once per frame
    public void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            playSoundShoot(_view.viewID);

            Vector3 rot = new Vector3(90, 180, 0);

            // player : y et w
            // cam

            Vector3 init = player.transform.rotation.eulerAngles;

            rot.y += init.y;

            GameObject shotBullet = Instantiate(bullet, shootPoint.transform.position, Quaternion.Euler(rot)) as GameObject;
            Vector3 force = player.transform.forward;
            force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * fpscam.transform.rotation.eulerAngles.x) * 2f, force.z);
            force *= power;
            shotBullet.GetComponent<Rigidbody>().AddForce(force);

            Destroy(shotBullet, time);

            void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.CompareTag("Floor"))
                {
                    Destroy(shotBullet, time);
                }
            }
            Destroy(shotBullet, time);
        }

    }
    public void TripleShoot()
    {
        if (Time.time > nextFire)
        {
            playSoundTripleShoot(_view.viewID);
            nextFire = Time.time + fireRate;
            for (int i = 0; i < 3; i++)
            {
                Vector3 point = new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z);

                Vector3 rot = new Vector3(90, 180, 0);

                // player : y et w
                // cam

                Vector3 init = player.transform.rotation.eulerAngles;

                rot.y += init.y;

                GameObject shotBullet = Instantiate(bullet, point, Quaternion.Euler(rot)) as GameObject;
                Vector3 force = player.transform.forward;
                force = new Vector3(force.x, -Mathf.Sin(Mathf.Deg2Rad * fpscam.transform.rotation.eulerAngles.x) * 2f, force.z);
                force *= power;
                shotBullet.GetComponent<Rigidbody>().AddForce(force);

                Destroy(shotBullet, time);

                void OnCollisionEnter(Collision collision)
                {
                    if (collision.gameObject.CompareTag("Floor"))
                    {
                        Destroy(shotBullet, time);
                    }
                }
                Destroy(shotBullet, time);
            }
        }

    }

    [PunRPC]
    void playSoundShoot(int viewID)
    {
        GameObject player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<AudioSource>().PlayOneShot(shootAudio);
        
        if (GetComponent<PhotonView>().isMine)
        {
            _view.RPC("playSoundShoot", PhotonTargets.OthersBuffered, viewID);
        }
    }
    [PunRPC]
    void playSoundTripleShoot(int viewID)
    {
        GameObject player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<AudioSource>().PlayOneShot(shootTripleAudio);
        
        if (GetComponent<PhotonView>().isMine)
        {
            _view.RPC("playSoundTripleShoot", PhotonTargets.OthersBuffered, viewID);
        }
    }
    
}