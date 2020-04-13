﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Win32;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
        public Transform pointShoot;

        public int gunDamage;//Damage of weapon , for the moment it's fixed but we need to redefine it
        public float fireRate;//Rate of weapon  
        public float weaponRange;//Range of wepon
        public float hitForce; //Knockback of weapon
        public float masse;
        public float distance;
        public Transform gunPosition;//Position of gun to calculate the line
        public Camera fpscam;

        public int power; //vitesse d'ejection
        public float time; //temps avant suppression de balle
        public GameObject bullet; //Balle utilisée
        
        //private AudioSource gunAudio; //The audio of gun
        [SerializeField] private LineRenderer gunLine; //The line of shoot 
        private float nextFire; // Time when you can do a new shot
        // Start is called before the first frame update

        // Update is called once per frame
        public void Shoot()
        {
            if (Time.time > nextFire)  //je ne pense pas que ça marche
            {
                GameObject shootedBullet = Instantiate(bullet, transform.position,Quaternion.identity) as GameObject;
                shootedBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
                Destroy(shootedBullet, time);
            }

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
        
        public float setDistance
        {
            set { hitForce = value; }
        }
}