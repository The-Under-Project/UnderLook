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
        //lobbyCamera.SetActive(false);
        //je dois instantier et désactiver les scriptes machins, voir la vidéo
        SceneManager.LoadScene("HeroesMaker", LoadSceneMode.Additive);

        Vector3 spawn = new Vector3(1, 15, 23);
            //GameObject.FindGameObjectWithTag("Respawn").transform;

        bool a = PhotonNetwork.Instantiate("Cassie", spawn, Quaternion.identity, 0);
        Debug.Log(a);
       
    }
    void OnLeftRoom()
    {
        Debug.Log("Left");
    }
    void OnLeftRoom()
    {
        Debug.Log("Left");
    }
}
