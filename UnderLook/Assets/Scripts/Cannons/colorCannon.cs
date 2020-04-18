using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorCannon : MonoBehaviour
{
    public bool isBlue;
    public string teamColor;
    void Start()
    {
        teamColor = isBlue ? "Blue" : "Red";
    }

}