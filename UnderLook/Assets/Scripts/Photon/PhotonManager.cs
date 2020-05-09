using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{
    public bool run = true;
    public string mapToLoad;
    private bool connected = false;
    public string instantiateName;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);


        // THE REASON WHY IT S NOT WORKING ONLINE

        PhotonNetwork.ConnectUsingSettings("1.0"); //remove if not debuging

        // THE REASON WHY IT S NOT WORKING ONLINE


        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 8 }, TypedLobby.Default); //dev mode
        Debug.Log("Created a room");
    }

    void OnJoinedLobby() //join lobby in GetRoomField
    {
        //SceneManager.LoadScene("AI", LoadSceneMode.Single);//dev mode

        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 8 }, TypedLobby.Default); //dev mode
        Debug.Log("Created a room");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined");
        //PhotonNetwork.player.NickName = GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text;

        connected = true;
        
    }
    void OnLeftRoom()
    {
        Debug.Log("Left");
    }
    private void Update()
    {
        if (run && connected)
        {
            run = false;
            SceneManager.LoadScene(mapToLoad, LoadSceneMode.Additive);

            Vector3 spawn = new Vector3(20, 200, 0);

            PhotonNetwork.player.NickName = GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName;

            PhotonNetwork.Instantiate(instantiateName, spawn, Quaternion.identity, 0); // <----------------------------------
            //PhotonNetwork.Instantiate("CassieMain", spawn, Quaternion.identity, 0);


            /*
            //Vector3 spawn = new Vector3(1, 15, 23);
            if (PhotonNetwork.countOfPlayers % 2 == 0)
                PhotonNetwork.Instantiate("Cassie", spawn, Quaternion.identity, 0);
            else
                PhotonNetwork.Instantiate("CassieR", spawn, Quaternion.identity, 0);*/
        }
    }
}
