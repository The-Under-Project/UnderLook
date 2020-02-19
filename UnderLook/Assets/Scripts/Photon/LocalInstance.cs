using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInstance : MonoBehaviour
{
    public bool workingOffline;
    public bool destroy;
    void Awake()
    {
        try
        { workingOffline = GameObject.FindGameObjectWithTag("GameController").GetComponent<LocalInstance>().workingOffline; }
        catch
        { workingOffline = true; }
        if (workingOffline)
        {
            Debug.Log(this.name);
            this.GetComponent<PlayerNetworkingDeactivate>().enabled = false;
        }
        if (destroy && !workingOffline)
        {
            Debug.Log("Destroyed!");
            Destroy(gameObject);
        }
    }
}
