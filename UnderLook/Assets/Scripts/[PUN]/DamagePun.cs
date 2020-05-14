﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePun : MonoBehaviour
{
    [PunRPC]
    void Dmg(Vector2 received)
    {
        int id = (int)received.x;
        int damage = (int)received.y;
        if (gameObject.GetComponent<Player.AI>() != null)
        {
            GetComponent<Player.AI>().hp -= damage;
            return;
        }
        if (gameObject.GetComponent<PhotonView>().viewID == id)
        {
            if     (GetComponent<Player.Cassie>() != null)
                GetComponent<Player.Cassie>().hp -= damage;
            else if (GetComponent<Player.Brik>() != null)
                GetComponent<Player.Brik>().hp -= damage;
            else if (GetComponent<Player.Yalee>() != null)
                GetComponent<Player.Yalee>().hp -= damage;
            else
                Debug.Log("ADD COMPONENT DAMAGE");
        }
           

    }
}
