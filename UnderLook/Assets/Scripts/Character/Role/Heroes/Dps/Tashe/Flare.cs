using System.Collections;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Flare : MonoBehaviour
{
    public GameObject roquette;
    public Rigidbody rb;
    public int rayon;
    public int nbRoquette;
    public int vitesse;
    private Vector3 ultPosition;
    private int GenPosition()
    {
        var rnd = new System.Random();
        return rnd.Next(rayon * 2 + 1) - rayon;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            ultPosition = rb.position;
            for (int i = 0; i < nbRoquette; i++)
            {
                Vector3 newPosition = ultPosition;
                newPosition.x += GenPosition();
                newPosition.z += GenPosition();
                newPosition.y += 200;
                GameObject instantiateRoquette = Instantiate(roquette, newPosition, Quaternion.identity);
                instantiateRoquette.GetComponent<Rigidbody>().velocity = Vector3.down*vitesse;
                
            }
        }
        Destroy(this);
    }
}
