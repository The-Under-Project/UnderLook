using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    public Cinemachine.CinemachinePathBase redPath;
    public Cinemachine.CinemachinePathBase bluePath;
    public bool run = false;


    public void Color(string name)
    {
        string payloadOwner = name;
        run = true;

        if(payloadOwner == "Blue")
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = bluePath;
            this.GetComponent<PayloadCountPlayer>().payloadOwner = name;
        }
        if (payloadOwner == "Red")
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = redPath;
            this.GetComponent<PayloadCountPlayer>().payloadOwner = name;
        }
        this.transform.parent = null;
    }
}
