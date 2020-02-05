using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayloadPUN : MonoBehaviour
{


    [PunRPC]
    void PayloadGetPath(string Message)
    {
        this.GetComponent<PayloadCountPlayer>().payloadOwner = Message;
        if (Message == "Blue")
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = this.GetComponent<PayloadOwner>().bluePath;
        }
        else
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = this.GetComponent<PayloadOwner>().redPath;
        }

        this.transform.parent = null;
    }
    [PunRPC]
    void PayloadPosition(float position)
    {
        if (!PhotonNetwork.isMasterClient)
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = position; //sync exactement le convoi avec la position
        }
    }
    [PunRPC]
    void PayloadSpeed(float speed)
    {
        this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = speed;
    }
    [PunRPC]
    void PayloadUnlock(string team)
    {
        this.GetComponent<PayloadOwner>().Color(team);
    }
}
