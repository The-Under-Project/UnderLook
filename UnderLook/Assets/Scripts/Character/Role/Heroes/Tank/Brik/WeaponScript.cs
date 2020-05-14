using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public bool activated;

    public float rotationSpeed;
    public float time = 0;
    public float timeMax = 1;

    void Update()
    {
        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("floor hit");
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
        }
        else
        {
            if(collision.gameObject.tag == "Player" && time < 0)
            {
                time = timeMax;
                Debug.Log("<color=blue>HIT PLAYER</color>");
                collision.transform.GetComponent<PhotonView>().photonView.RPC("Dmg", PhotonTargets.All, collision.transform.GetComponent<PhotonView>().viewID);
            }

            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
        }
    }
}
