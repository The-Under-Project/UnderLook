using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")] //make a sub menu
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;

    public float maxhp;
    public float maxshield;

    public float speed;
    public float gravity;
    public float jumpspeed;



    public float coolDownCap1;
    public float damageCap1;
    public float durationCap1;
    public float duracionCap1;

    public float coolDownCap2;
    public float damageCap2;
    public float durationCap2;
    public float duracionCap2;

    public float coolDownCap3;
    public float damageCap3;
    public float durationCap3;
    public float duracionCap3;


    public float ultimateCD;
    public float ultimateDamge;
    public float ultimateDuration;
    public float duracionUltimate;

}
