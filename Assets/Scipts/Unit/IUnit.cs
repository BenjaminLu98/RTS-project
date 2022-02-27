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

    public float AttackRange
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
    /// Rotate to the target and move to the target position with animation. 
    /// </summary>
    /// <param name="worldPosition">target position</param>
    /// <param name="speed"></param>
    public void moveTo(Vector3 worldPosition, float speed);
    //Move to the target position with animation
    public void moveTo(int x, int z, float speed);

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
    public bool receiveDamage(IUnit.DamageType type, CombatData from);

}
