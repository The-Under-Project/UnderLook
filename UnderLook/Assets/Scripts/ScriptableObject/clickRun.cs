using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickRun : MonoBehaviour
{
   public void Click()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PhotonManager>().instantiateName = GetComponentInParent<HeroDisplay>().nameText.text;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PhotonManager>().run = true;

        GameObject[] finish = GameObject.FindGameObjectsWithTag("Finish");
        foreach (var f in finish)
        {
            Destroy(f);
        }
        
        
    }
}
