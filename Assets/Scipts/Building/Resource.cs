using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public abstract class Resource : MonoBehaviour,IPlaceableObj
{
    private uint resourceAmount = 10000;


    protected PositionInfo positionInfo;
    protected bool isObstacle;
    public Vector2Int Position { 
        get
        {
            return new Vector2Int(positionInfo.x,positionInfo.z);
        }
        private set { } 
    }

    public bool IsObstacle { 
        get { 
            return true; 
        }
        set
        {
            isObstacle = value;
        }
    }
    public Vector2Int Size
    {
        get
        {
            return new Vector2Int(positionInfo.width,positionInfo.height);
        }
        set
        {
            positionInfo.width = value.x;
            positionInfo.height = value.y;
        }
    }

    public uint ResourceAmount { get => resourceAmount; set => resourceAmount = value; }

    public bool placeAt(int x, int z)
    {
        bool isSucess = GridSystem.current.setValue(x, z, 100, this, positionInfo.width, positionInfo.height);
        if (isSucess)
        {
            Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
            transform.position = truePosition;

            GridSystem.current.removeValue(positionInfo.x, positionInfo.z, positionInfo.width, positionInfo.height);

            positionInfo.x = x;
            positionInfo.z = z;

            return true;
        }
        return false;
    }

    public bool placeAt(Vector3 worldPosition)
    {
        bool isSuccess = GridSystem.current.setValue(worldPosition, 100, this, positionInfo.width, positionInfo.height);
        if (isSuccess)
        {
            int x, z;
            GridSystem.current.getXZ(worldPosition, out x, out z);
            Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
            transform.position = truePosition;

            //GridSystem.current.removeValue(this.x, this.z, width, height);
            positionInfo.x = x;
            positionInfo.z = z;

            return true;
        }
        return false;
    }


    public uint getResource(uint expectedNum)
    {
        if (resourceAmount >= expectedNum)
        {
            resourceAmount -= expectedNum;
            return expectedNum;
        }
        else
        {
            resourceAmount = 0;
            return 0;

        }
    }
}
