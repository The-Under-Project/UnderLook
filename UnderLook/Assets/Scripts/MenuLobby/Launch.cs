using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("GameMaster");
                /*
                GameObject[] players = GameObject.FindGameObjectsWithTag("CubeMenu");
                foreach (var player in players) //peut être il va se dc lui même
                {
                    player.GetComponent<PhotonView>().RPC("Change", PhotonTargets.All, "blue");
                }*/

            }
            else
            {
                Debug.Log("Not GameMaster");
            }
        }

    }
}
