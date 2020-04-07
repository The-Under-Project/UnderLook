using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadCountPlayer : MonoBehaviour
{
    [HideInInspector] public PhotonView photonView;

    public string payloadOwner = "";

    public float size = 7.5f;
    public float speed = 0.015f;

    public GameObject[] players;
    public GameObject[] capturingRed = new GameObject[4];
    public GameObject[] capturingBlue = new GameObject[4];

    private int cappingBlue;
    private int cappingRed;

    public int returnCappingRed;  //this make sure unity doesn't have lag because capping frop to 0 every update
    public int returnCappingBlue; //this make sure unity doesn't have lag because capping frop to 0 every update

    void Start()
    {

        photonView = GetComponent<PhotonView>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        CapInfo();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, size - 3);
    }

    void CapInfo()
    {
        (cappingRed, cappingBlue) = (0, 0); //restart
        foreach (GameObject player in players)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < size)
            {
                if (player.GetComponent<Player.AI>() != null)
                {
                    if (player.GetComponent<Player.AI>().teamColor == "Blue")
                    {
                        capturingBlue[cappingBlue] = player;
                        cappingBlue += 1;
                    }
                    else
                    {
                        capturingRed[cappingRed] = player;
                        cappingRed += 1;
                    }
                }
                else
                {
                    if (player.GetComponent<TeamColor>().teamColor == "Blue")
                    {
                        capturingBlue[cappingBlue] = player;
                        cappingBlue += 1;
                    }
                    else
                    {
                        capturingRed[cappingRed] = player;
                        cappingRed += 1;
                    }
                }
            }
        }

        returnCappingRed = cappingRed;
        returnCappingBlue = cappingBlue;
        CapTimer();
    }

    void CapTimer()
    {
        if (returnCappingBlue >= 1 && returnCappingRed == 0 && payloadOwner == "Blue")
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = speed;
            
        }
        else if (returnCappingRed >= 1 && returnCappingBlue == 0 && payloadOwner == "Red")
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = speed;
        }
        else
        {
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = 0;
        }
        if (PhotonNetwork.inRoom)
        {
            photonView.RPC("PayloadPosition", PhotonTargets.AllBuffered, this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position);
            //actualise position convoi autre joueurs
            photonView.RPC("PayloadSpeed", PhotonTargets.AllBuffered, this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed);
        }
    }
}
