using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")]public bool DEBUG_LOCAL = true;
    private bool start = true;
    public Vector3 direction;

    [Header("Global")]

    public Vector3 offset;
    private Vector3 firstPos;
    private Vector3 normal;


    public string teamColor = "Blue";
    public Boolean setColor;

    [HideInInspector]public Rigidbody rb;
    public float percentage = 0;
    public float time;

    public int range;
    public GameObject[] playerWallHack;
    public GameObject[] oldWallHack;


    public bool canYouGetMyColor = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(teamColor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
    
    void DEBUG(bool d)
    {
        DEBUG_LOCAL = d;
    }
}
