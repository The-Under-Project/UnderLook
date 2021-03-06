﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class AI : Base //IA doit hériter à la fin, c'est celle qui n'est pas abstract 
    {
        private NavMeshAgent agent;

        public float Timer;
        public float groupTimer;
        private float actualTime;
        public float actualGroupTimer;
        public float RandomTime;

        public bool BlueTeam = false;
        public string teamColor = "";
        public string enemyColor = "";

        public float distance = 4.5f;
        public GameObject[] players;

        public bool isGrouping;


        #region Main

        void Start()
        {
            photonView = GetComponent<PhotonView>();
            teamColor = BlueTeam ? "Blue" : "Red";
            enemyColor = BlueTeam ? "Red" : "Blue";
            int i = 0;
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var mate in players)
            {
                if (mate.name == this.name) //check name 
                {
                    (players[i], players[players.Length -1]) = (players[players.Length -1], players[i]); //je le place à la fin
                    //comme ça je peux ne pas y accéder dans mes boucles for
                    break;
                }
                i++;
            }


            agent = this.GetComponent<NavMeshAgent>();

            
        }
        void MoveToPayload()
        {
            GameObject point = GameObject.FindGameObjectWithTag("Point");
            GameObject payload = GameObject.FindGameObjectWithTag("Payload");

            float d1 = Random.Range(-distance, distance);
            float d2 = Random.Range(-distance, distance); //random pour uin peu plus de mouvement elle sont refresh à la fin du timer

            if (payload.GetComponent<PayloadOwner>().run)
            {
                    agent.SetDestination(payload.transform.position + new Vector3(d1, 0, d2));
                //move to payload
            }
            else
            {
                
                    agent.SetDestination(point.transform.position + new Vector3(d1, 0, d2)); //move to point
                
            }
        }
        void GroupUp()
        {
            GameObject point = null;
            if (teamColor == "Blue")
                point = GameObject.FindGameObjectWithTag("GroupBlue");
            else
                point = GameObject.FindGameObjectWithTag("GroupRed");
            agent.SetDestination(point.transform.position);
        }

        void Look() //gte the closest enemie and look at him
        {
            float d = float.MaxValue;
            GameObject closest = null;
            try
            {
                foreach (var enemie in players)
                {
                    if (enemie.GetComponent<AI>() != null)
                    {
                        if (enemie.GetComponent<AI>().enemyColor == teamColor) //pas ouf pcq j'accède à IA au lieu de player, il faut faire de l'héritage
                        {
                            float localD = Vector3.Distance(enemie.transform.position, this.transform.position);
                            if (localD < d)
                            {
                                d = localD;
                                closest = enemie;

                            }
                        }
                    }
                }
            transform.LookAt(closest.transform);
            }
            catch // pas ouf ^
            {

            }
        }

        private void Update()
        {
            DoTime();
            Look();
            //transform.LookAt(player.transform);
            if (this.hp < 0)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }


        private void DoTime()
        {
            if(actualTime <= 0)
            {
                actualTime = Timer + Random.Range(-RandomTime, RandomTime);
                if(isGrouping)
                    GroupUp();
                else
                    MoveToPayload();
            }
            else
            {
                actualTime -= Time.deltaTime;
            }

            if (actualGroupTimer <= 0)
            {
                actualGroupTimer = groupTimer;
                GameObject payload = GameObject.FindGameObjectWithTag("Payload");
                if ((payload.GetComponent<PayloadCountPlayer>().returnCappingBlue == 0 && teamColor == "Blue") ||
                    (payload.GetComponent<PayloadCountPlayer>().returnCappingRed == 0 && teamColor == "Red")) 
                    isGrouping = true;
            }
            else if (!isGrouping)
            {
                actualGroupTimer -= Time.deltaTime;
            }
        }
        #endregion Main

    }
}
