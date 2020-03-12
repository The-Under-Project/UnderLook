using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRooms : MonoBehaviour
{
    public float timerefresh;
    public float timemax;
    private bool refresh = true;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }
    void FixedUpdate()
    {
        if (refresh)
        {
            refresh = false;
            this.GetComponent<Text>().text = "";
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                this.GetComponent<Text>().text = this.GetComponent<Text>().text + "Name of the room: " + game.name +
                    " Players : " + game.playerCount + "/" + game.MaxPlayers + "\n";
            }
        }else if (timerefresh > 0)
        {
            timerefresh -= Time.deltaTime;
        }
        else
        {
            timerefresh = timemax;
            refresh = true;
        }

    }
}
