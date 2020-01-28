using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsApply : MonoBehaviour
{
    public Material basic, wallHack;
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material = basic;
    }
}
