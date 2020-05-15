using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCollision : MonoBehaviour
{
    public string insName;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "noCollision")
        {
            Debug.Log("Should Instantiate");
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            PhotonNetwork.Instantiate(insName, transform.position, transform.rotation, 0);

            if (collision.gameObject.tag == "Player")
            {

                Debug.Log("<color=blue>HIT PLAYER with dagger damage = </color>" + damage);
                Vector2 send = new Vector2(collision.transform.GetComponent<PhotonView>().viewID, damage);
                collision.transform.GetComponent<PhotonView>().photonView.RPC("Dmg", PhotonTargets.All, send);

                Destroy(gameObject);
            }
        }
    }
}
