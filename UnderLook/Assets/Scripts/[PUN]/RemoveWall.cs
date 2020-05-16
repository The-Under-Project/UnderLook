using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWall : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<StartingPun>().canStart)
        {
            Destroy(gameObject);
        }
    }
}
