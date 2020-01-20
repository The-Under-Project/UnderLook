using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lobbyCamera;
    public GameObject spawn;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnJoinedLobby() //join lobby in GetRoomField
    {
    PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(){ MaxPlayers = 8}, TypedLobby.Default); //dev mode
    Debug.Log("Created a room");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined");
        //PhotonNetwork.Instantiate("Character/Capsule", spawn.transform.position, Quaternion.identity, 0);
        //lobbyCamera.SetActive(false);
    }
}
