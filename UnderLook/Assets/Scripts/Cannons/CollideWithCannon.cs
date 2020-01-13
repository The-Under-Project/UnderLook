using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithCannon : MonoBehaviour
{
    public GameObject dollycart;
    private void OnTriggerEnter(Collider other) //if collide with nametag cannon
    {
        if (other.tag == "Cannon")
        {
            gameObject.transform.SetParent(dollycart.transform);
            gameObject.GetComponent<Movement>().isOnTrack = true;
            gameObject.GetComponent<Movement>().DOLaunch();
        }
    }
}
