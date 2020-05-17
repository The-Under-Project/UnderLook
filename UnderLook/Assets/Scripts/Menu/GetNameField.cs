using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GetNameField : MonoBehaviour
{
    private float waitmarket = 1f;
    public void Check()
    {
        if (GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text.Length <= 15 && 
            GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text.Length >= 3)
        {

            Wait();
            GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName = 
                GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>().text;
        }
        
    }

    Sequence Wait()
    {
        waitmarket = 0f;
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => waitmarket, x => waitmarket = x, 1, 100f));
        return s;
    }
}
