using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DestroyWhenName : MonoBehaviour
{
    private float waitmarket = 1f;
    void FixedUpdate()
    {
        if(GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName != "")
        {
            Destroy(gameObject);
        }
    }
}
