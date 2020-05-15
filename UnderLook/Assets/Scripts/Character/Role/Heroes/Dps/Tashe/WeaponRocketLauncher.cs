using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class WeaponRocketLauncher : BulletShot
    {
        void CallWeapon()
        {
            this.Shoot();
        }
    }
}