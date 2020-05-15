using System.Collections;
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
            if (GetComponent<Player.Cassie>() != null)
                GetComponent<Player.Cassie>().hp -= damage;
            else if (GetComponent<Player.Brik>() != null)
                GetComponent<Player.Brik>().hp -= damage;
            else if (GetComponent<Player.Yalee>() != null)
                GetComponent<Player.Yalee>().hp -= damage;
            else if (GetComponent<Player.Timtry>() != null)
                GetComponent<Player.Timtry>().hp -= damage;
            else if (GetComponent<Player.Easwith>() != null)
                GetComponent<Player.Easwith>().hp -= damage;
            else if (GetComponent<Player.Roy>() != null)
                GetComponent<Player.Roy>().hp -= damage;
            else
                Debug.Log("ADD COMPONENT DAMAGE");
        }


    }
    [PunRPC]
    void Teleport(Quaternion received)
    {
        int id = (int)received.x;
        Vector3 position = new Vector3(received.y, received.z, received.w);
        if (gameObject.GetComponent<PhotonView>().viewID == id)
        {
            gameObject.GetComponent<Moving>().Teleport(position);
        }
    }
}
