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
    //move in seconds.
    public float moveSpeed;
    public float rotateSpeed;

    public CombatData(CombatData data)
    {
        hp = data.hp;
        physicalAttack = data.physicalAttack;   
        magicAttack = data.magicAttack;
        physicalDefence = data.physicalDefence;
        magicDefence = data.magicDefence;
        mana = data.mana;
        attackInterval = data.attackInterval;
        attackRange = data.attackRange;
        moveSpeed = data.moveSpeed;
        rotateSpeed = data.rotateSpeed;
    }
}
