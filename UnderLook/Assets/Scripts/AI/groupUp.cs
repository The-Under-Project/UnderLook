using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groupUp : MonoBehaviour
{
    [HideInInspector] public PhotonView photonView;

    public GameObject[] players;
    public GameObject[] capturingColor = new GameObject[8];

    public bool isBlue;
    public string teamColor;

    private int cappingC;

    private int size = 5;


    void Start()
    {
        teamColor = isBlue ? "Blue" : "Red";
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        CapInfo();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, size);
    }

    void CapInfo()
    {
        int max = 0;
        cappingC = 0; //restart
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player.AI>() != null)
            {
                if (player.GetComponent<Player.AI>().teamColor == teamColor)
                {
                    max += 1;
                    if (Vector3.Distance(player.transform.position, this.transform.position) < size)
                    {
                        capturingColor[cappingC] = player;
                        cappingC += 1;
                    }
                }
            }
        }
        if (max == cappingC)
        {
            foreach (GameObject player in players)
            {
                if (player.GetComponent<Player.AI>() != null)
                {
                    if (player.GetComponent<Player.AI>().teamColor == teamColor)
                    {
                        player.GetComponent<Player.AI>().isGrouping = false;
                    }
                }
            }
        }
    }
}
