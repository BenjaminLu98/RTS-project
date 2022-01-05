using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    public int HP => throw new System.NotImplementedException();

    GridSystem gridSystem;
    public GridSystem GridSystem {
        set
        {
            gridSystem = value;
        }
    }

    protected int x;
    protected int z;
    public Vector2Int Position
    {
        get
        {
            return new Vector2Int(x, z);
        }
    }

    protected bool isObstacle;
    public bool IsObstacle {
        get
        {
            return isObstacle;
        }
        set
        {
            isObstacle = value;
        }
    }

    protected int width;
    protected int height;
    public Vector2Int Size {
        set
        {
            width = value.x;
            height = value.y;
        }
        get
        {
            return new Vector2Int(width, height);
        }
    }

    public void moveTo(Vector3 worldPosition)
    {
        throw new System.NotImplementedException();
    }

    public void MoveTo(int x, int z)
    {
        throw new System.NotImplementedException();
    }

    protected Animator animator;
    public Animator Animator
    {
        get
        {
            return animator;
        }
    }


    public bool placeAt(int x, int z)
    {
        if (gridSystem == null)
        {
            Debug.Log(this.GetType().Name + ": gridSystem not loaded!");
            return false;
        }
        else
        {
            bool isSucess = gridSystem.setValue(x, z, new GridData(100, this));
            if (isSucess)
            {
                Vector3 truePosition = gridSystem.getWorldPosition(x, z);
                transform.position = truePosition;

                gridSystem.removeValue(this.x, this.z, width, height);

                this.x = x;
                this.z = z;
                return true;
            }
            return false;
        }
    }

    public bool placeAt(Vector3 worldPosition)
    {
        if (gridSystem == null)
        {
            Debug.Log(this.GetType().Name + ": gridSystem not loaded!");
            return false;
        }
        else
        {
            bool isSuccess = gridSystem.setValue(worldPosition, new GridData(100, this), width, height);
            if (isSuccess)
            {
                int x, z;
                gridSystem.getXZ(worldPosition, out x, out z);
                Vector3 truePosition = gridSystem.getWorldPosition(x, z);
                transform.position = truePosition;

                gridSystem.removeValue(this.x, this.z, width, height);
                this.x = x;
                this.z = z;
                return true;
            }
            return false;
        }
    }

}
