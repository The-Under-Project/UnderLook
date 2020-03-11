using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamColor : MonoBehaviour
{
    [SerializeField ] private bool isBlue;
    //go faire un debug
    [HideInInspector] public string teamColor;
    [HideInInspector] public string enemieColor;

    void Start()
    {
        teamColor = isBlue ? "Blue" : "Red";
        enemieColor = !isBlue ? "Red" : "Blue";
    }
}
