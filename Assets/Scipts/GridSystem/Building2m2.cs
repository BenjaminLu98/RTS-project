using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building2m2 : MonoBehaviour,IBuilding
{
    protected List<GameObject> trainableUnits;
    public List<GameObject> TrainableUnits { 
        get
        {
            return trainableUnits;
        }
        set
        {
            trainableUnits = value;
        }
    }

    public abstract void train(int index);
    

    protected GridSystem gridSystem;
    public GridSystem GridSystem { 
        set
        {
            gridSystem = value;
        }
    }

    protected int hp;
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

    protected bool isObtacle=true;
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

    protected int width =2;
    protected int height =2;
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
    protected int x;
    //z of grid position
    protected int z;
    public Vector2Int Position
    {
        get
        {
            return new Vector2Int(x, z);
        }
    }

    protected IBuilding.dir currentDir=IBuilding.dir.backward;
    public IBuilding.dir CurrentDir
    {
        get
        {
            return currentDir;
        }
    }

    protected GameObject prefab;
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
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

    public virtual void placeAt(int x, int z)
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

    public virtual void placeAt(Vector3 worldPosition)
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

    public void produce(int index)
    {
        if (index < 0 || index > trainableUnits.Count)
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + " :index out of range");
        }

        Vector2Int TargetGrid = gridSystem.getBlankGrid(new Vector2Int(x, z), width, height);
        Vector3 targetPosition = gridSystem.getWorldPosition(TargetGrid.x, TargetGrid.y);
        if (gridSystem.checkOccupation(TargetGrid.x, TargetGrid.y))
        {
            GameObject unit = Instantiate(trainableUnits[index], targetPosition, Quaternion.identity);
            Unit placeableComponent = unit.GetComponent<Unit>();
            //Can I update the grid date at another place?
            gridSystem.setValue(TargetGrid.x, TargetGrid.y, new GridData(99, placeableComponent), placeableComponent.Size.x, placeableComponent.Size.y);
        }
    }
}
