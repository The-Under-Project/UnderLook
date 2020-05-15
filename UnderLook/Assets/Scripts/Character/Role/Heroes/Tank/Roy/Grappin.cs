using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappin : MonoBehaviour
{
    [Header("Online")]
    public Vector3 pos;
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject playerAd = collision.gameObject;
            Quaternion send = new Quaternion(collision.transform.GetComponent<PhotonView>().viewID, pos.x, pos.y, pos.z);
            collision.transform.GetComponent<PhotonView>().photonView.RPC("Teleport", PhotonTargets.All, send);
        }
    }
}