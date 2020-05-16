using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWalls : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<StartingPun>().canStart)
        {
            Destroy(gameObject);
        }
    }
}