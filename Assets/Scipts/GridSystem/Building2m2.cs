using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building2m2 : MonoBehaviour,IBuilding
{
    private List<IUnit> trainableUnits;
    public List<IUnit> TrainableUnits { 
        get
        {
            return trainableUnits;
        }
        set
        {
            trainableUnits = value;
        }
    }

    private GridSystem gridSystem;
    public GridSystem GridSystem { 
        set
        {
            gridSystem = value;
        }
    }

    int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }

    public void damage(int damageAmount)
    {
        hp -= damageAmount;
        if (hp < 0)
        {
            onDestroy();
        }
    }

    public virtual void onDestroy()
    {
        Destroy(this, 2f);
    }

    private bool isObtacle=true;
    public bool IsObstacle
    {
        get
        {
            return isObtacle;
        }
        set
        {
            isObtacle = value;
        }
    }

    int width=2;
    int height=2;
    public Vector2Int Size
    {
        get
        {
            return new Vector2Int(width, height);
        }
        set
        {
            width = value.x;
            height = value.y;
        }
    }

    //x of grid position
    int x;
    //z of grid position
    int z;
    public Vector2Int Position
    {
        get
        {
            return new Vector2Int(x, z);
        }
    }

    IBuilding.dir currentDir;
    public IBuilding.dir CurrentDir
    {
        get
        {
            return currentDir;
        }
    }

    public void rotate()
    {
        switch (currentDir)
        {
            case IBuilding.dir.backward:
                currentDir = IBuilding.dir.right;
                break;
            case IBuilding.dir.right:
                currentDir = IBuilding.dir.forward;
                break;
            case IBuilding.dir.forward:
                currentDir = IBuilding.dir.left;
                break;
            case IBuilding.dir.left:
                currentDir = IBuilding.dir.backward;
                break;
        }
        transform.GetChild(0).Rotate(Vector3.forward, -90f);

    }

    public void placeAt(int x, int z)
    {
        if (gridSystem == null) Debug.Log(this.GetType().Name + ": gridSystem not loaded!");
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
            }
        }
    }

    public void placeAt(Vector3 worldPosition)
    {
        if (gridSystem == null) Debug.Log(this.GetType().Name + ": gridSystem not loaded!");
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
            }
            
        }
    }
    
    
}
