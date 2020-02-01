using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class CassieWeapon : HitScanShoot
    {
        public int gunDamageChild; //Damage of weapon , for the moment it's fixed but we need to redefine it
        public float fireRateChild; //Rate of weapon 
        public float weaponRangeChild; //Range of wepon
        public float hitForceChild; //Knockback of weapon 
        private void Start()
        {
            this.setGunDamage = gunDamageChild;
            this.setFireRate = fireRateChild;
            this.setWeaponRange = weaponRangeChild;
            this.setHitForce = hitForceChild;
        }
        void CallWeapon()
        {
            this.Shoot();
        }
    }
}
