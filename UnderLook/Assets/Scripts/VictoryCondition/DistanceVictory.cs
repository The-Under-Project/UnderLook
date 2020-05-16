using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceVictory : MonoBehaviour
{
    public bool END;
    public float distance;
    public GameObject[] victoriesPoint;

    private void Start()
    {
        victoriesPoint = GameObject.FindGameObjectsWithTag("VictoryPoint");        
    }

    void Update()
    {
        distance = Math.Min(Vector2.Distance(gameObject.transform.position, victoriesPoint[0].transform.position),
            Vector2.Distance(gameObject.transform.position, victoriesPoint[1].transform.position));
        if (distance <= 0.5f)
        {
            END = true;
        }
    }
}
