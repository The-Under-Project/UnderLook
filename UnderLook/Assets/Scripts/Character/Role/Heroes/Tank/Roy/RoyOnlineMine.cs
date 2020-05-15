using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoyOnlineMine : MonoBehaviour
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
            if (mine.GetComponent<RoyMine>().canYouGetMyColor)
            {
                enemieColor = mine.GetComponent<RoyMine>().enemieColor;
                Destroy(mine);
                break;
            }
        }
        GameObject[] Roy = GameObject.FindGameObjectsWithTag("Player");
        foreach (var r in Roy)
        {
            if (r.GetComponent<Player.Roy>() != null)
            {
                if (r.GetComponent<Player.Roy>().enabled == true && r.GetComponent<Player.Roy>().hasInstantiateAMine)
                {
                    owner = r;
                    r.GetComponent<Player.Roy>().hasInstantiateAMine = false;
                    break;
                }
            }
        }

        colorBub = "OrangeBubble";
        PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
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
        }
        else
        {
            timeCD -= Time.deltaTime;
        }
    }
}
