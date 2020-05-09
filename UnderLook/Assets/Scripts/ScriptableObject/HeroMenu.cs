using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Hero", menuName ="Hero Menu")] //make a sub menu
public class HeroMenu : ScriptableObject
{
    public string instantiateName;
    public Sprite artwork;
}
