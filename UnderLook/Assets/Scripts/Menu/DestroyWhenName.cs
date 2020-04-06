using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyWhenName : MonoBehaviour
{
    void FixedUpdate()
    {
        if(GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName != "")
        {
            Destroy(gameObject);
        }
    }
}
