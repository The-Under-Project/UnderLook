using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSetup : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
