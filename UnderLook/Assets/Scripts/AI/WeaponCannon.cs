using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class WeaponCannon : BulletShot
    {
        void CallWeapon()
        {
            this.Shoot();
        }
    }
}
