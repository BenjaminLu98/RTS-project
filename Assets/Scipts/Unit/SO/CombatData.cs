using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Combat Data", menuName ="Character Combat Data")]
public class CombatData : ScriptableObject
{
    [Header("Stats")]
    public float hp;
    public float physicalAttack;
    public float magicAttack;
    public float physicalDefence;
    public float magicDefence;
    public float mana;
    public float attackInterval;
    public int attackRange;
}
