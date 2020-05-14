using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePun : MonoBehaviour
{
    [PunRPC]
    void Dmg(int id)
    {
        Debug.Log(id);
        Debug.Log("");
        Debug.Log(gameObject.GetComponent<PhotonView>().viewID);
        Debug.Log("");
        if (gameObject.GetComponent<Player.AI>() != null)
        {
            GetComponent<Player.AI>().hp -= 50;
            return;
        }
        if (gameObject.GetComponent<PhotonView>().viewID == id)
        {
            if (GetComponent<Player.Cassie>()!= null)
                GetComponent<Player.Cassie>().hp -= 50;
            else if (GetComponent<Player.Brik>()!= null)
                GetComponent<Player.Brik>().hp -= 50;
        }
           

    }
}
