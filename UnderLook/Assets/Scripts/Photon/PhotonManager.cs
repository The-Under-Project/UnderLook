using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : Photon.MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnJoinedLobby() //join lobby in GetRoomField
    {
    //SceneManager.LoadScene("AI", LoadSceneMode.Single);//dev mode
    PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(){ MaxPlayers = 8}, TypedLobby.Default); //dev mode
    Debug.Log("Created a room");
    }
    
    void OnJoinedRoom()
    {
        Debug.Log("Joined");
        //PhotonNetwork.Instantiate("Character/Capsule", spawn.transform.position, Quaternion.identity, 0);
        //lobbyCamera.SetActive(false);
    }
    void OnLeftRoom()
    {
        Debug.Log("Left");
    }
}
