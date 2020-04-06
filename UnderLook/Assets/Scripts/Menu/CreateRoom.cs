using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour
{
public void create()
    {
        GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().roomName = "pog";
    }
}
