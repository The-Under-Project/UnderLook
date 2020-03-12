using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScriptNetwork : MonoBehaviour
{


    // Update is called once per frame
    [PunRPC]
    void Change(string color)
    {

        //PhotonNetwork.LeaveRoom();
        //Debug.Log("Left!");
        //PhotonNetwork.Destroy(gameObject);
        //SceneManager.LoadScene("HeroesMaker");
        Vector3 spawn = new Vector3(10, 2, 0); //new Vector3(1, 15, 23);
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 8 }, TypedLobby.Default); //dev mode
        Debug.Log("Change!");
        if (color == "blue")
            PhotonNetwork.Instantiate("Cassie", spawn, Quaternion.identity, 0);
        else
            PhotonNetwork.Instantiate("CassieR", spawn, Quaternion.identity, 0);
        SceneManager.LoadScene("HeroesMaker", LoadSceneMode.Additive);
        PhotonNetwork.Destroy(gameObject);



        GameObject[] o = GameObject.FindGameObjectsWithTag("Destroy");
        foreach (var obj in o) 
        {
            Destroy(obj);
        }
    }
}
