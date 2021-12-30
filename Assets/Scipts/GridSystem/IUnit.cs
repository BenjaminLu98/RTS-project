using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit :IPlaceableObj
{
    int HP
    {
        get;
    }

    Animator Animator
    {
        get;
    }

    //Move to the target position with animation
    public void moveTo(Vector3 worldPosition);
    //Move to the target position with animation
    public void MoveTo(int x, int z);

    

}
