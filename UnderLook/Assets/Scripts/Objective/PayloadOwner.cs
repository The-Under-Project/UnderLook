using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadOwner : MonoBehaviour
{
    public Cinemachine.CinemachinePathBase redPath;
    public Cinemachine.CinemachinePathBase bluePath;
    public bool run = false;

    public string payloadOwner;

    [HideInInspector] public PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

        public void Color(string name)
    {
        payloadOwner = name;
        run = true;

        if(payloadOwner == "Blue" && PhotonNetwork.inRoom)
        {
            photonView.RPC("PayloadGetPath", PhotonTargets.AllBuffered, "Blue");
        }
        if (payloadOwner == "Red" && PhotonNetwork.inRoom)
        {
            photonView.RPC("PayloadGetPath", PhotonTargets.AllBuffered, "Red");
        }
    }
}
