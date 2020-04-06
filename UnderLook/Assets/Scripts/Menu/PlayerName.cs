using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public string playerName;
    public string roomName;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
