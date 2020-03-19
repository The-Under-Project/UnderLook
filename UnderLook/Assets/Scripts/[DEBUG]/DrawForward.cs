using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawForward : MonoBehaviour
{
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(gameObject.transform.position, transform.forward);
    }
}
