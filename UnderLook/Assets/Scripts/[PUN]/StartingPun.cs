using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPun : MonoBehaviour
{
    public bool canStart;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("<color=green>MATCH IS STARTING</color>");

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                GameObject[] walls   = GameObject.FindGameObjectsWithTag("WallDisable");

                foreach (var player in players)
                {
                    player.transform.GetComponent<PhotonView>().photonView.RPC("STARTING_MATCH", PhotonTargets.All, true);
                }

                foreach (var wall in walls)
                {
                    wall.transform.GetComponent<PhotonView>().photonView.RPC("STARTING_MATCH", PhotonTargets.All, true);
                }

                PhotonNetwork.room.IsOpen = false;
            }
            else
            {
                Debug.Log("<color=red>MATCH IS NOT STARTING</color>");
            }
        }
    }

    [PunRPC]
    void STARTING_MATCH(bool t)
    {
        canStart = true;
    }
}
