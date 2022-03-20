using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IPlaceableObj
{
    /// <summary>
    /// The units that can be produced by this building
    /// </summary>

    public enum dir {forward,left,backward,right};

    int HP
    {
        get;
    }


    public void train(int index);

    /// <summary>
    /// Immediately produce a unit at a grid that is not occupied.
    /// </summary>
    /// <param name="index">the index in the trainableUnits list</param>
    public void produce(int index);

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
