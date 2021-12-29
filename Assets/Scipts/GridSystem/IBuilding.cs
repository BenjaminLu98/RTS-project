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

    

}
