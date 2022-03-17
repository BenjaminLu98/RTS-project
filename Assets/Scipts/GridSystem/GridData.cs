using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeType
{
    Base,
    Grass
}

[System.Serializable]
public class GridData
{
    [SerializeField]
    private int num;
    [SerializeField]
    private bool isOccupied;

    private List<Cube> cube;
    // Lowest height is 0, normally it is 1. Actual height should be height*sideLength
    [SerializeField]
    private int height=1;
    [SerializeField]
    private Vector2Int position;
    private IPlaceableObj placeable;

    private int gCost;
    private int hCost;
    private int fCost;
    public GridData comeFromNode;

    public int Num { get => num; set => num = value; }
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
    public List<Cube> Cube { get => cube; set => cube = value; }
    public int GCost { get => gCost; set => gCost = value; }
    public int HCost { get => hCost; set => hCost = value; }
    public int FCost { get => fCost; set => fCost = value; }
    public int Height { get => height; set => height = value; }
    public Vector2Int Position { get => position; set => position = value; }
    public IPlaceableObj PlaceableObj { get => placeable; set => placeable = value; }

    public GridData()
    {
        this.Num = 0;
        Position = new Vector2Int(0, 0);
    }

    // Start is called before the first frame update
    public GridData(int num)
    {
        this.Num = num;
        Position = new Vector2Int(0, 0);
    }
    public GridData(int num, Vector2Int position, IPlaceableObj placeableObj, int height)
    {
        this.Num = num;
        this.height = height;
        this.Position = position;
    }

    public GridData(int num, Vector2Int position, int height, CubeType cubeType)
    {
        this.Num = num;
        this.height = height;
        cube = new List<Cube>();
        this.Position = position;

        var worldPosition = new Vector3(position.x * GridSystem.current.sideLength, height * GridSystem.current.sideLength, position.y * GridSystem.current.sideLength) + GridSystem.current.origin;

        for (int i = 0; i < height; i++)
        {
            cube.Add(new Cube(CubeType.Base, worldPosition));
        }
        cube.Add(new Cube(cubeType, worldPosition));
    }

    public GridData(GridSystem gs, int num, Vector2Int position, IPlaceableObj placeableObj, int height, CubeType cubeType)
    {
        this.Num = num;
        this.height = height;
        cube = new List<Cube>();
        this.Position = position;

        var worldPosition = new Vector3(position.x * GridSystem.current.sideLength, height * GridSystem.current.sideLength, position.y * GridSystem.current.sideLength) + GridSystem.current.origin;

        for (int i = 0; i < height-1; i++)
        {
            cube.Add(new Cube(CubeType.Base, worldPosition));
        }
        cube.Add(new Cube(cubeType, worldPosition));
    }

    public void calculateFCost()
    {
        fCost = gCost + hCost;
    }

}



public class Cube
{
    const int GRASS_VARIANT_POSSIBILITY = 5;

    CubeType cubeType;
    GameObject prefab;
    GameObject instance;
    public Cube(CubeType cubeType, Vector3 worldPosition)
    {
        var parent = getParent();

        RandomlyLoadCubeType(cubeType);
        if (!prefab) Debug.LogError("Fail to load resource:" + cubeType.ToString());
        instance = GameObject.Instantiate(prefab, worldPosition, Quaternion.identity, parent.transform);
    }

    public void DestroyInstance()
    {
        if (instance != null)
        {
            GameObject.DestroyImmediate(instance);
        }
    }

    // Return a valid parent.
    GameObject getParent()
    {
        var parent = GameObject.Find("Cubes");
        if (parent == null)
        {
            var grandParent = GameObject.Find("Map");
            if (!grandParent)
            {
                grandParent = new GameObject("Map");
            }
            parent = new GameObject("Cubes");
            parent.transform.parent = grandParent.transform;
        }
        return parent;
    }

    /// <summary>
    /// This method enriches the cubeType by adding some variant according to GRASS_VARIANT_POSSIBILITY. 
    /// Resource cube should be stored in the original type folder. e.g. Map/Grass/....
    /// The original cube should be stored with index 0, and the variant should use other indices. e.g. Map/Grass/Grass0.prefab
    /// </summary>
    /// <param name="cubeType">The original cubeType.</param>
    private void RandomlyLoadCubeType(CubeType cubeType)
    {
        // TODO: resouce should only load once. Extract it to another class.
        var prefabList = Resources.LoadAll<GameObject>("Map/" + cubeType.ToString() + "/");
        var VariantRand = Random.Range(0, 100);
        if (prefabList.Length == 1 || VariantRand < 100 - GRASS_VARIANT_POSSIBILITY)
        {
            prefab = prefabList[0];
        }
        else
        {
            var whichVariantRand = Random.Range(1, prefabList.Length);
            prefab = prefabList[whichVariantRand];
        }
    }

}