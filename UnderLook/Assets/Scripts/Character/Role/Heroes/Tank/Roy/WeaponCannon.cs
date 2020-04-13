using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wapon
{
    public class WeaponCannon : BulletShot
    {
        void CallWeapon()
        {
            this.Shoot();
        }
    }
}
