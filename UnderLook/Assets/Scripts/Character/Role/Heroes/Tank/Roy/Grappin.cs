using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappin : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")]public bool DEBUG_LOCAL;
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
    public Boolean draw = false;
    public GameObject shield;
    private Boolean activateRotation = true;
    private GameObject playerL;

    public bool canYouGetMyColor = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            activateRotation = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject playerAd = collision.gameObject;
            playerAd.GetComponent<Rigidbody>().position = playerL.GetComponent<Rigidbody>().position;
        }
    }

    public void setPlayer(GameObject player)
    {
        playerL = player;
    }

    void DEBUG(bool d)
    {
        DEBUG_LOCAL = d;
    }
}
