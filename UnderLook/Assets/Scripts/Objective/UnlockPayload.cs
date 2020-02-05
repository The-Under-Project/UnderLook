using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPayload : MonoBehaviour
{
    [HideInInspector] public PhotonView photonView;
    public int timeToUnlock;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
        void Update()
    {
        float blue = this.gameObject.GetComponentInParent<PointCountPlayers>().timerBlue;
        float red = this.gameObject.GetComponentInParent<PointCountPlayers>().timerRed;

        if (blue > timeToUnlock && PhotonNetwork.inRoom)
        {
            photonView.RPC("PayloadUnlock", PhotonTargets.AllBuffered, "Blue"); //donne le propriétaire du convoi au serveur
            Destroy(this);
        }
        else if (red > timeToUnlock && PhotonNetwork.inRoom)
        {
            photonView.RPC("PayloadUnlock", PhotonTargets.AllBuffered, "Red");
            Destroy(this);
        }
    }
}
