using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IPlaceableObj
{
    /// <summary>
    /// The units that can be produced by this building
    /// </summary>
    List<IUnit> TrainableUnits
    {
        get;
        set;
    }

    Vector2Int Size
    {
        get;
        set;
    }
    public enum dir {forward,left,backward,right};

    int HP
    {
        get;
    }

    public void damage(int damageAmount);

    public void onDestroy();

    /// <summary>
    /// Rotate the building in a sequence: backward,right,forward,left
    /// </summary>
    public void rotate();

    dir CurrentDir
    {
        get;
    }

}
