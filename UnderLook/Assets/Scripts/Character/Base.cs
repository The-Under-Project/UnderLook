using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class Base : MonoBehaviour
    {

        [Header("Online")]
        [Tooltip("If you want a local version, activate it")]public bool DEBUG;
        //[HideInInspector]
        public PhotonView photonView;

        [Header("Base")]
        public int hpmax;
        public int hp;
        public int speed;
        public int jumpspeed;

        public void Awake()
        {
            hp = hpmax;


            //PhotonView photonView = PhotonView.Get(this);

            //if (DEBUG)
            //    gameObject.GetComponent<PhotonView>().enabled = false;
        }
    }
}
