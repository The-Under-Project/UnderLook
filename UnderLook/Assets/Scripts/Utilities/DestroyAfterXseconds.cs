using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXseconds : MonoBehaviour
{
    public float time;
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
