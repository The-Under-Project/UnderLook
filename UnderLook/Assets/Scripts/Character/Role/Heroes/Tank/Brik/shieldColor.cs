using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldColor : MonoBehaviour
{
    public Material[] Colors;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = gameObject.GetComponentInParent<TeamColor>().teamColor == "Blue" ? Colors[0] : Colors[1];
    }
}
