using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDebug : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 force;
    private Rigidbody rb;
    public Vector3 firstPos;
    public Vector3 normal;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        firstPos = collision.contacts[0].point;
        rb.isKinematic = true;
        normal = gameObject.transform.localPosition - firstPos + offset;

        transform.rotation = Quaternion.LookRotation(normal);

        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        //gameObject.transform.localRotation = normal;
        //gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    public void ins()
    {
        transform.rotation = Quaternion.LookRotation(normal);
        PhotonNetwork.Instantiate("mine2", this.transform.position, this.transform.rotation, 0);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(gameObject.transform.localPosition + offset, 0.25f);
        //Gizmos.DrawLine(gameObject.transform.localPosition, firstPos);
        //Debug.DrawRay(gameObject.transform.localPosition, firstPos);
    }
}
