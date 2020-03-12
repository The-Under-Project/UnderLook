using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetRoomField : MonoBehaviour
{
    public string scn;
    public void Check()
    {
        if (GameObject.FindGameObjectWithTag("RoomField").GetComponent<Text>().text.Length <= 15 &&
            GameObject.FindGameObjectWithTag("RoomField").GetComponent<Text>().text.Length >= 3)
        {
            GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().roomName =
                GameObject.FindGameObjectWithTag("RoomField").GetComponent<Text>().text;

            //PhotonNetwork.Disconnect();
            SceneManager.LoadScene(scn);
            PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 4 }, TypedLobby.Default);
        }
    }
}
