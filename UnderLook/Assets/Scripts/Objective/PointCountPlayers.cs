using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCountPlayers : MonoBehaviour
{
    public float size = 7.5f;

    public GameObject[] players;
    public GameObject[] capturingRed = new GameObject[4];
    public GameObject[] capturingBlue = new GameObject[4];

    private int cappingBlue;
    private int cappingRed;

    public int returnCappingRed;  //this make sure unity doesn't have lag because capping frop to 0 every update
    public int returnCappingBlue; //this make sure unity doesn't have lag because capping frop to 0 every update

    public float timerRed;
    public float timerBlue;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        CapInfo();
        CapTimer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
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
                if (player.GetComponent<Player.AI>().teamColor == "Blue")
                {
                    capturingBlue[cappingBlue] = player;
                    cappingBlue += 1;
                }
                else
                {
                    capturingBlue[cappingRed] = player;
                    cappingRed += 1;
                }
            }
        }
        returnCappingRed = cappingRed;
        returnCappingBlue = cappingBlue;
    }

    void CapTimer()
    {
        if (returnCappingBlue >= 1 && returnCappingRed == 0)
        {
            timerBlue += Time.deltaTime;
        }
        else if (returnCappingRed >= 1 && returnCappingBlue == 0)
        {
            timerRed += Time.deltaTime;
        }
    }
}
