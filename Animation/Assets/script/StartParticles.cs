using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticles : MonoBehaviour
{
    public GameObject child;
    public void particles()
    {
        child.SetActive(true);
    }
}
