using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerIA : MonoBehaviour
{
    public int resultBlue, resultRed;
    public GameObject BlueIA, RedIA;
    void Update()
    {
        int redI = 0;
        int blueI = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            if (p.GetComponent<TeamColor>().teamColor == "Blue")
            {
                blueI += 1;
            }
            else
            {
                redI += 1;
            }
        }
        resultBlue = blueI;
        resultRed = redI;
        Player();
    }
    void Player()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PhotonNetwork.isMasterClient)
            {
                Vector3 bSpawn = GameObject.FindGameObjectWithTag("bluePos").transform.position;
                Vector3 rSpawn = GameObject.FindGameObjectWithTag("redPos").transform.position;
                for (int i = 0; i < 4-resultBlue; i++)
                {
                    Instantiate(BlueIA, bSpawn, Quaternion.identity);
                }
                for (int i = 0; i < 4 - resultRed; i++)
                {
                    Instantiate(RedIA, rSpawn, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
    
}
