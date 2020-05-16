using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotateCamera : MonoBehaviour
{
    public GameObject fpscam;
    void Update()
    {
        gameObject.transform.rotation = fpscam.transform.rotation;
    }
}
