using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mine : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")]public bool DEBUG_LOCAL;
    private bool start = true;
    public Vector3 direction;

    [Header("Global")]

    public string enemieColor;

    [HideInInspector]public Rigidbody rb;
    public float percentage = 0;
    public float time;

    public int range;
    public GameObject[] playerWallHack;
    public GameObject[] oldWallHack;


    public bool canYouGetMyColor = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            rb.isKinematic = true;

            if (start && !DEBUG_LOCAL)
            {
                start = false;
                PhotonNetwork.Instantiate("mine2", this.transform.position, this.transform.rotation, 0);
            }
            /////Destroy(gameObject);
            //DOTween.Play(Des());
        }
    }
    
    void SetColor(string color)
    {
        enemieColor = color;

    }
    public void starting(Vector3 force, string enemyCol)
    {
        direction = force;
        Debug.Log(enemyCol);
        SetColor(enemyCol);
    }
    void DEBUG(bool d)
    {
        DEBUG_LOCAL = d;
    }


}
