using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictory2 : MonoBehaviour
{
    public GameObject payload;
    public GameObject victory;
    public GameObject defeat;
    void Update()
    {
        if(payload == null)
            payload = GameObject.FindGameObjectWithTag("Payload");


        if (payload.GetComponent<DistanceVictory>().END &&  GetComponent<PhotonView>().isMine)
        {
            if(payload.GetComponent<PayloadOwner>().payloadOwner == GetComponent<TeamColor>().teamColor)
            {
                Instantiate(victory, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Instantiate(defeat, Vector3.zero, Quaternion.identity);
            }
            Destroy(this);
        }
    }
}
