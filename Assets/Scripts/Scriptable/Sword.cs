using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nova Espada", menuName = "Espadas")]
public class Sword : ScriptableObject
{
    public string name;
    public string description;

    public Sprite artwork;

    public int minAttackDamage;
    public int maxAttackDamage;

    [Range(0,2)]
    [Tooltip("0 = No Sword\n1 = Short Sword\n2 = Long Sword")]
    public int SwordType;
}
