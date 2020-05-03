using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimtryMaterialDeath : MonoBehaviour
{
    public Material death;
    public int hpdebug;
    void FixedUpdate()
    {
        hpdebug = gameObject.GetComponent<Health.PlayerManagerTimTry>().Health;
        if (hpdebug <= 0)
        {
            gameObject.GetComponent<Renderer>().material = death;
        }
    }
}
