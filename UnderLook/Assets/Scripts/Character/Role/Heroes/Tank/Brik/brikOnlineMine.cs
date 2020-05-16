using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class brikOnlineMine : MonoBehaviour
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
    public GameObject[] playerCantMove;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Start()
    {
        DOTween.Play(Des());

        GameObject[] mineInit = GameObject.FindGameObjectsWithTag("mine");
        foreach (var mine in mineInit)
        {
            if (mine.GetComponent<mineBrik>().canYouGetMyColor)
            {
                enemieColor = mine.GetComponent<mineBrik>().enemieColor;

                Destroy(mine);
                break;
            }
        }
        GameObject[] Cassies = GameObject.FindGameObjectsWithTag("Player");
        foreach (var cassie in Cassies)
        {
            if (cassie.GetComponent<Player.Brik>() != null)
            {
                if (cassie.GetComponent<Player.Brik>().enabled == true && cassie.GetComponent<Player.Brik>().hasInstantiateAMine)
                {
                    owner = cassie;
                    cassie.GetComponent<Player.Brik>().hasInstantiateAMine = false;
                    break;
                }
            }
        }

        if (enemieColor == "Blue")
            colorBub = "BlueBubble";
        else
            colorBub = "RedBubble";
        if(owner != null)
            PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
    }


    private void FixedUpdate()
    {
        GameObject[] playerCantMove = GameObject.FindGameObjectsWithTag("Player");
        if (percentage > 0.01f && percentage < 100)
        {
            foreach (var player in playerCantMove)
            {
                if (Vector3.Distance(gameObject.transform.position, player.transform.position) < range)
                    player.GetComponent<Moving>().enabled = false;
            }
        }
        if (percentage >= 100)

        {

            // avant de destroy il faut changer le shader
            foreach (var player in playerCantMove)
            {
                if (player != null)
                {
                    player.GetComponent<Moving>().enabled = true;
                    player.GetComponent<PlayerNetworkingDeactivate>().Initialize();
                }
            }
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
    Sequence Des()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentage, x => percentage = x, 100, time));
        return s;
    }
}
