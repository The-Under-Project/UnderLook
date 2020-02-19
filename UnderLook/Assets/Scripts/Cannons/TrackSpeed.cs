using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpeed : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = player.GetComponent<Moving>().launch; //sync speed
    }
}
