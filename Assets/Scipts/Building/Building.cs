using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour,IBuilding
{
    protected List<GameObject> trainableUnits;
    [SerializeField] protected GameObject previewPrefab;
    protected ResourceLoader rl;

    protected void Start()
    {
        gameObject.tag = "Building";
        gameObject.layer = 7;
        trainableUnits = new List<GameObject>();
        rl = FindObjectOfType<ResourceLoader>();
    }

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

    public void train(int index)
    {

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

    protected int width ;
    protected int height ;
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

    /// <summary>
    /// rotate around the y axis in counter clockwise direction
    /// </summary>
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

    /// <summary>
    /// place the object at grid coordinate(x,z) and replace the value in the grids. The size of the rectangle is specified by width and height.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>true if the grid system successfully place the building at the this position</returns>
    public virtual bool placeAt(int x, int z)
    {
        bool isSucess = GridSystem.current.setValue(x, z, 100, this);
        if (isSucess)
        {
            Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
            transform.position = truePosition;

            GridSystem.current.removeValue(this.x, this.z, width, height);

            this.x = x;
            this.z = z;

            return true;
        }
        return false;
    }

    /// <summary>
    /// place the object at worldPosition and replace the value in the grids. The size of the rectangle is specified by width and height.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns>true if the grid system successfully place the building at the this position</returns>
    public virtual bool placeAt(Vector3 worldPosition)
    {
        bool isSuccess = GridSystem.current.setValue(worldPosition, 100, this, width, height);
        if (isSuccess)
        {
            int x, z;
            GridSystem.current.getXZ(worldPosition, out x, out z);
            Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
            transform.position = truePosition;

            //GridSystem.current.removeValue(this.x, this.z, width, height);
            this.x = x;
            this.z = z;

            return true;
        }
        return false;  
    }

    /// <summary>
    /// generate a unit immediately around the building
    /// </summary>
    /// <param name="index">the index of the unit in trainable unit list</param>
    public void produce(int index)
    {
        if (index < 0 || index > trainableUnits.Count)
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + " :index out of range");
        }

        Vector2Int TargetGrid = GridSystem.current.getBlankGrid(new Vector2Int(x, z), width, height);
        Vector3 targetPosition = GridSystem.current.getWorldPosition(TargetGrid.x, TargetGrid.y);
        if (GridSystem.current.checkOccupation(TargetGrid.x, TargetGrid.y))
        {
            GameObject unit = Instantiate(trainableUnits[index], targetPosition, Quaternion.identity);
            Unit placeableComponent = unit.GetComponent<Unit>();
            //Can I update the grid date at another place?
            //GridSystem.current.setValue(TargetGrid.x, TargetGrid.y, 99, placeableComponent, placeableComponent.Size.x, placeableComponent.Size.y);
            placeableComponent.placeAt(TargetGrid.x, TargetGrid.y);

        }
    }
}
