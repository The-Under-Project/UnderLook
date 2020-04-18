using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShieldOnline : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")] public bool DEBUG_LOCAL;
    private bool start = true;
    private GameObject owner;
    public Vector3 direction;

    [Header("Global")]

    public string enemieColor;
    public string colorBub;

    [HideInInspector] public Rigidbody rb;
    public float percentage = 0;
    public float time;

    public int range;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Start()
    {
        DOTween.Play(Des());

        
        GameObject[] Roys = GameObject.FindGameObjectsWithTag("Player");
        foreach (var roy in Roys)
        {
            if (roy.GetComponent<Player.Roy>() != null)
            {
                if (roy.GetComponent<Player.Roy>().enabled == true && roy.GetComponent<Player.Roy>().hasInstantiateAShield)
                {
                    owner = roy;
                    roy.GetComponent<Player.Roy>().hasInstantiateAShield = false;
                    break;
                }
            }
        }
        if(enemieColor == "Blue")
            colorBub = "BlueBubble";
        else if (enemieColor == "Red")
            colorBub = "RedBubble";
        else
            Debug.Log("Wtf you re wrong");
        PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
    }

    Sequence Des()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentage, x => percentage = x, 100, time));
        return s;
    }
}
