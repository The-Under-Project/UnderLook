using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")] //make a sub menu
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;
}
