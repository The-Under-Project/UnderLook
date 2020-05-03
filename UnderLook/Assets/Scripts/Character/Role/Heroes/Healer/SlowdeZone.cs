﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdeZone : MonoBehaviour
{
    public GameObject zone; // prefab de la zone
    public GameObject jesuisinstancier;
    public GameObject[] ennemiteam = new GameObject[4];

    public List<GameObject> collisionedwith = new List<GameObject>();
    public List<float> speedinit = new List<float>();

    public float duration;
    public float puissanceSlowance;

    public void slowzone()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 positionfinale = hit.point;
            positionfinale.y += 0.4f;
            jesuisinstancier = Instantiate(zone, positionfinale, Quaternion.identity);
        }
    }

    public void IsInContact()
    {
        
        foreach (var joueur in ennemiteam)
        {
            
            if (joueur.GetComponent<Moving>() != null && Mathf.Abs(joueur.transform.position.x - jesuisinstancier.transform.position.x) <= 1 && Mathf.Abs(joueur.transform.position.z - jesuisinstancier.transform.position.z) <= 1f )
            {
               
                
                if (joueur.GetComponent<Moving>().tempsrestant == 0)
                { 
                    joueur.GetComponent<Moving>().speed *= puissanceSlowance;
                }
                joueur.GetComponent<Moving>().factor = puissanceSlowance;
                joueur.GetComponent<Moving>().ChangeSpeedAtEndOfTIime(duration, false);
                
            }
            
        }
        
        
    }
    public void CreateEnnemiTeam(string ally)
    {
        GameObject[] alljoueur = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach(var joeur in alljoueur)
        {
            if(joeur.GetComponent<TeamColor>().enemieColor == ally)
            {
                ennemiteam[i] = joeur;
                i++;
            }
            else
            {
                ennemiteam[i] = joeur;
            }
        }
    }
}
