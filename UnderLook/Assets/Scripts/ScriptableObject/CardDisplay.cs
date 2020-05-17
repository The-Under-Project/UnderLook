using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text descriptionText;
    public Image artwork;
    void Start() //set ui
    {
        nameText.text = card.cardName; 
        descriptionText.text = card.description;
        artwork.sprite = card.artwork;
    }
}
