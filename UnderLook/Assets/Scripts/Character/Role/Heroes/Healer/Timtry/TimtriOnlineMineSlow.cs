using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimtriOnlineMineSlow : MonoBehaviour
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
            if (mine.GetComponent<mineSlow>().canYouGetMyColor)
            {
                enemieColor = mine.GetComponent<mineSlow>().enemieColor;

                Destroy(mine);
                break;
            }
        }
        GameObject[] Timtri = GameObject.FindGameObjectsWithTag("Player");
        foreach (var Tim in Timtri)
        {
            if (Tim.GetComponent<Player.Timtry>() != null)
            {
                if (Tim.GetComponent<Player.Timtry>().enabled == true && Tim.GetComponent<Player.Timtry>().hasInstantiateAMineSLOW)
                {
                    owner = Tim;
                    Tim.GetComponent<Player.Timtry>().hasInstantiateAMineSLOW = false;
                    break;
                }
            }
        }

        if (enemieColor == "Blue")
            colorBub = "BlueBubble";
        else
            colorBub = "RedBubble";
        if (owner != null)
            PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
        else
            Debug.Log("No owner");
    }


    private void FixedUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in players)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < range)
            {
                if(player.GetComponent<Moving>().speed == player.GetComponent<Moving>().originalSpeed)
                {
                    player.GetComponent<Moving>().speed /= 3;
                }
            }
        }


        if (percentage >= 100)

        {

            // avant de destroy il faut changer le shader
            foreach (var player in players)
            {
                if (player != null && player.GetComponent<Moving>().speed < player.GetComponent<Moving>().originalSpeed)
                {
                    player.GetComponent<Moving>().speed = player.GetComponent<Moving>().originalSpeed;
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
