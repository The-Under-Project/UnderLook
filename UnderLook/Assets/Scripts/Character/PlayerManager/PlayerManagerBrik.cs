using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class PlayerManagerBrik : Photon.PunBehaviour, IPunObservable
    {
        public GameObject shield;
        public bool shieldState;
        [Tooltip("The current Health of our player")]
        public int Health = 5000;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public void Awake()
        {
            if (photonView.isMine)
            {
                LocalPlayerInstance = gameObject;
            }
        }
        public void Update()
        {

            if (Input.GetKeyDown(KeyCode.M))
            {
                GetComponent<Player.Brik>().hp -= 50;
            }
            if (photonView.isMine)
            {
                if (this.GetComponent<Player.Brik>().hp <= 0f)
                {
                    Debug.Log("Death");
                    GetComponent<Player.Brik>().hp = GetComponent<Player.Brik>().hpmax;
                    GetComponent<Moving>().TP(GetComponent<TeamColor>().isBlue);
                }
            }
            Health = GetComponent<Player.Brik>().hp;
            shieldState = shield.activeSelf;
        }
        public void Damage()
        {
            GetComponent<Player.Brik>().hp -= 50;
            Debug.Log("Player Hit");
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            Debug.Log("<color=green>called</color>");
            if (stream.isWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(this.Health);
                stream.SendNext(gameObject.GetComponent<TeamColor>().isBlue);
                stream.SendNext(GetComponent<shieldState>().state);
            }
            else
            {
                // Network player, receive data
                this.Health = (int)stream.ReceiveNext();
                gameObject.GetComponent<TeamColor>().isBlue = (bool)stream.ReceiveNext();
                shieldState = (bool)stream.ReceiveNext(); 
                shield.SetActive(shieldState);
                Debug.Log("Received: " + shieldState);
            }
            gameObject.GetComponent<TeamColor>().Up();
        }
    }
}
