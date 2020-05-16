using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
public class RaslaOnlineMine : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")] public bool DEBUG_LOCAL;
    private bool start = true;
    private GameObject owner;
    public Vector3 direction;

    [Header("Global")]
    public int dmg;
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
            if (mine.GetComponent<mineRasla>().canYouGetMyColor)
            {
                enemieColor = mine.GetComponent<mineRasla>().enemieColor;

                Destroy(mine);
                break;
            }
        }
        GameObject[] Timtri = GameObject.FindGameObjectsWithTag("Player");
        foreach (var Tim in Timtri)
        {
            if (Tim.GetComponent<Player.Rasla>() != null)
            {
                if (Tim.GetComponent<Player.Rasla>().enabled == true && Tim.GetComponent<Player.Rasla>().hasInstantiateAMine)
                {
                    owner = Tim;
                    Tim.GetComponent<Player.Rasla>().hasInstantiateAMine = false;
                    break;
                }
            }
        }

        if (enemieColor == "Blue")
            colorBub = "BlueBubble";
        else if (enemieColor == "Red")
            colorBub = "RedBubble";
        else
            Debug.Log(enemieColor);

        if (owner != null)
            PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
        else
            Debug.Log("No owner");
    }


    private void FixedUpdate()
    {
        timer();
        if (percentage >= 100)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

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
            Debug.Log("mineAction " + PhotonNetwork.isMasterClient);
            foreach (var player in players)
            {
                
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) < range)
                {
                    if ( PhotonNetwork.isMasterClient)
                    {
                        Vector2 send = new Vector2(player.transform.GetComponent<PhotonView>().viewID, dmg);
                        player.GetComponent<PhotonView>().photonView.RPC("Dmg", PhotonTargets.All, send);
                    }
                }
            }
        }
        else
        {
            timeCD -= Time.deltaTime;
        }
    }
}
