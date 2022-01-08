using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit :IPlaceableObj
{
    int HP
    {
        get;
    }

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

    public float AttackSpeed
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
    /// Rotate to the target and move to the target position with animation
    /// </summary>
    /// <param name="worldPosition">target position</param>
    /// <param name="speed"></param>
    public void moveTo(Vector3 worldPosition, float speed);
    //Move to the target position with animation
    public void MoveTo(int x, int z, float speed);

    


}
