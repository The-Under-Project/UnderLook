using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotateCamera : MonoBehaviour
{
    public GameObject fpscam;
    public Vector3 offsetinit;
    void Update()
    {
        Vector3 rotate = offsetinit + fpscam.transform.rotation.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(rotate);
    }
}
