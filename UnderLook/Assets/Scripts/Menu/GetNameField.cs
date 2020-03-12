using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNameField : MonoBehaviour
{
    public void Check()
    {
        if (GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text.Length <= 15 && 
            GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text.Length >= 3)
        {

            
            GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName = 
                GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text;
        }
    }
}
