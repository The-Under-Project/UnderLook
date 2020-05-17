using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Player;

public class TimtriOnlineMine : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")] public bool DEBUG_LOCAL;
    private bool start = true;
    private GameObject owner;
    public Vector3 direction;

    [Header("Global")]
    public int heal;
    public string enemieColor;
    public string colorBub;

    [HideInInspector] public Rigidbody rb;
    public float percentage = 0;
    public float time;
    public float timeCD;
    public float timeCDmax;

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
            if (mine.GetComponent<mineHeal>().canYouGetMyColor)
            {
                enemieColor = mine.GetComponent<mineHeal>().enemieColor;

                Destroy(mine);
                break;
            }
        }
        GameObject[] Timtri = GameObject.FindGameObjectsWithTag("Player");
        foreach (var Tim in Timtri)
        {
            if (Tim.GetComponent<Player.Timtry>() != null)
            {
                if (Tim.GetComponent<Player.Timtry > ().enabled == true && Tim.GetComponent<Player.Timtry > ().hasInstantiateAMine)
                {
                    owner = Tim;
                    Tim.GetComponent<Player.Timtry > ().hasInstantiateAMine = false;
                    break;
                }
            }
        }

        colorBub = "GreenBubble";
        if (owner != null)
            PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
        else
            Debug.Log("No owner");
        heal = (int)owner.GetComponent<Timtry>().heal;
    }


    private void FixedUpdate()
    {
        timer();
        if (percentage >= 100)
        {
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
    void timer()
    {
        if (timeCD < 0)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            timeCD = timeCDmax;

            foreach (var player in players)
            {
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) < range && PhotonNetwork.isMasterClient)
                {
                    Vector2 send = new Vector2(player.transform.GetComponent<PhotonView>().viewID, -heal);
                    player.GetComponent<PhotonView>().photonView.RPC("Dmg", PhotonTargets.All, send);
                }
            }
        }
        else
        {
            timeCD -= Time.deltaTime;
        }
    }
}
