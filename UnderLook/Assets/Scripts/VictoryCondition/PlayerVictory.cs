using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource audioSource = null;
    [SerializeField] private AudioClip Win = null;
    [SerializeField] private AudioClip Loose = null;

    [Header("Settings")]
    public GameObject payload;
    public GameObject victory;
    public GameObject defeat;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(payload == null)
            payload = GameObject.FindGameObjectWithTag("Payload");


        if (payload.GetComponent<DistanceVictory>().END &&  GetComponent<PhotonView>().isMine)
        {
            if(payload.GetComponent<PayloadOwner>().payloadOwner == GetComponent<TeamColor>().teamColor)
            {
                Instantiate(victory, Vector3.zero, Quaternion.identity);
                audioSource.PlayOneShot(Win);
            }
            else
            {
                Instantiate(defeat, Vector3.zero, Quaternion.identity);
                audioSource.PlayOneShot(Loose);
            }
            Destroy(this);
        }
    }
}
