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
                    PhotonNetwork.Instantiate("blueIA", bSpawn, Quaternion.identity, 0);
                }
                for (int i = 0; i < 4 - resultRed; i++)
                {
                    PhotonNetwork.Instantiate("redIA", rSpawn, Quaternion.identity, 0);
                    //Instantiate(RedIA, rSpawn, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (PhotonNetwork.isMasterClient)
            {
                Vector3 rSpawn = GameObject.FindGameObjectWithTag("redPos").transform.position;
                PhotonNetwork.Instantiate("redIA", rSpawn, Quaternion.identity, 0);
            }
        }
    }
    
}
