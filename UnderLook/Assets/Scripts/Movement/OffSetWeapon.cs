using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSetWeapon : MonoBehaviour
{
    public GameObject Weapon;
    public bool changeweapondUp;
    public bool changeweapondDown;


    void Update()
    {
        if (changeweapondUp)
        {
            Weapon.transform.position = new Vector3(Weapon.transform.position.x, Weapon.transform.position.y + 0.5f, Weapon.transform.position.z);
            changeweapondUp = false;
        }
        if (changeweapondDown)
        {
            Weapon.transform.position = new Vector3(Weapon.transform.position.x, Weapon.transform.position.y - 0.5f, Weapon.transform.position.z);
            changeweapondDown = false;
        }
    }
}
