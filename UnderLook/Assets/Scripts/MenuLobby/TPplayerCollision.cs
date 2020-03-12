using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPplayerCollision : MonoBehaviour
{
    public bool isBlue;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<TeamColor>().isBlue = isBlue;
            other.GetComponent<Moving>().TP();
        }
    }
}
