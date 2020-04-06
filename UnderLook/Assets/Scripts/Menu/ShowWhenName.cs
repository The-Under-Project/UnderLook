using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWhenName : MonoBehaviour
{
    public bool thename = false;
    private void Start()
    {
        if (this.GetComponent<Text>() != null)
        {
            this.GetComponent<Text>().enabled = false;
        }
        if (this.GetComponent<SpriteRenderer>() != null)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (this.GetComponent<Image>() != null)
        {
            this.GetComponent<Image>().enabled = false;
        }
        if (this.GetComponent<Button>() != null)
        {
            this.GetComponent<Button>().enabled = false;
        }
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName != "")
        {
            if (this.GetComponent<Text>() != null)
            {
                if (thename)
                {
                    this.GetComponent<Text>().text =
                        GameObject.FindGameObjectWithTag("PlayerPref").GetComponent<PlayerName>().playerName;
                }
                this.GetComponent<Text>().enabled = true;
            }
            if (this.GetComponent<SpriteRenderer>() != null)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (this.GetComponent<Image>() != null)
            {
                this.GetComponent<Image>().enabled = true;
            }
            if (this.GetComponent<Button>() != null)
            {
                this.GetComponent<Button>().enabled = true;
            }
            Destroy(this);
        }
    }
}
