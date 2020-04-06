﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassieMaterialDeath : MonoBehaviour
{
    public Material death;
    public int hpdebug;
    void FixedUpdate()
    {
        hpdebug = gameObject.GetComponent<Health.PlayerManagerCassie>().Health;
        if (hpdebug <= 0)
        {
            gameObject.GetComponent<Renderer>().material = death;
        }
    }
}
