using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsApply : MonoBehaviour
{
    public Material[] color;
    public Material basic, wallHack;
    public void Launch()
    {
        if (gameObject.GetComponent<TeamColor>().teamColor == "Blue")
        {
            basic = color[0];
        }
        else
        {
            basic = color[1];
        }
        gameObject.GetComponent<Renderer>().material = basic;
    }
}
