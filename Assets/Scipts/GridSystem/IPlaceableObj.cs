using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceableObj 
{
    /// <summary>
    /// Place the object in the grid using XZ. 
    /// It will use grid System to find the true world position of this object and then use it to update the position.
    /// </summary>
    /// <param name="x">the x coordinate in grid system</param>
    /// <param name="z">the z coordinate in grid system</param>
    public void placeAt(int x, int z);

    /// <summary>
    /// Place the object in the grid using WoldPosition. 
    /// It will use grid System to find the true world position of this object and then use it to update the position
    /// </summary>
    /// <param name="worldPosition">The world Position of the object</param>
    public void placeAt(Vector3 worldPosition);

    /// <summary>
    /// save the reference of the gridSystem
    /// </summary>
    GridSystem GridSystem
    {
        set;
    }

    Vector2Int Position
    {
        get;
    }

    /// <summary>
    /// whether or not a path(the grid chain from one grid to another grid) can contain a grid if the object is placed into this grid.
    /// </summary>
    bool IsObstacle
    {
        get;
        set;
    } 
}
