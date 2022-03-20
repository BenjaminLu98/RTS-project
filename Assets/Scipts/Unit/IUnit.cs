using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit :IPlaceableObj
{
    public enum dir { forward, left, backward, right };
    public enum DamageType{
        Physical,
        Magic
    };
    public enum DefenseType
    {
        Physical,
        Magic
    };

    public float PhysicalAttack
    {
        get;
        set;
    }
    public float MagicAttack
    {
        get;
        set;
    }

    public float PhysicalDefence
    {
        get;
        set;
    }

    public float MagicDefence
    {
        get;
        set;
    }

    public float Mana
    {
        get;
        set;
    }

    public float AttackInterval
    {
        get;
        set;
    }

    public int AttackRange
    {
        get;
        set;
    }

    public float MaxSpeed
    {
        get;
    }

    Animator Animator
    {
        get;
    }

    /// <summary>
    /// Attack the gameObject at position(x,z). If there is no unit in the target posiiton, do nothing.
    /// </summary>
    public float DealDamage(DamageType type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expectedDamage"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public float receiveDamage(IUnit.DamageType type, CombatData from);

}
