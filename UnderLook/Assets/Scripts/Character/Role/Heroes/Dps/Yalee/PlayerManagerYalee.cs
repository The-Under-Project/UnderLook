using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class PlayerManagerYalee : Photon.PunBehaviour, IPunObservable
    {
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
                GetComponent<Player.Yalee>().hp -= 50;
            }
            if (photonView.isMine)
            {
                if (this.GetComponent<Player.Yalee>().hp <= 0f)
                {
                    Debug.Log("Health negative");
                    //Destroy(gameObject);
                }
            }
            Health = GetComponent<Player.Yalee>().hp;
        }
        public void Damage()
        {
            GetComponent<Player.Yalee>().hp -= 50;
            Debug.Log("Player Hit");
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(this.Health);
                stream.SendNext(gameObject.GetComponent<TeamColor>().isBlue);
            }
            else
            {
                // Network player, receive data
                this.Health = (int)stream.ReceiveNext();
                gameObject.GetComponent<TeamColor>().isBlue = (bool)stream.ReceiveNext();
            }
            gameObject.GetComponent<TeamColor>().Up();
        }
    }
}