using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroDisplay : MonoBehaviour
{
    public HeroMenu card;

    public Text nameText;
    public Image artwork;
    void Start() //set ui
    {
        nameText.text = card.name;
        artwork.sprite = card.artwork;
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<PhotonManager>().run)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
