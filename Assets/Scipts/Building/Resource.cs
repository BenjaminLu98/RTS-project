using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : IPlaceableObj
{
    public Vector2Int Position => throw new System.NotImplementedException();

    public bool IsObstacle { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector2Int Size { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool placeAt(int x, int z)
    {
        throw new System.NotImplementedException();
    }

    public bool placeAt(Vector3 worldPosition)
    {
        throw new System.NotImplementedException();
    }

}
