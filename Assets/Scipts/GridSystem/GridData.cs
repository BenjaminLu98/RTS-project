using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeType
{
    Base,
    Grass
}
public class GridData
{
    private int num;
    private IPlaceableObj placeableObj;
    private bool isOccupied;
    private List<Cube> cube;
    // Lowest height is 0, normally it is 1. Actual height should be height*sideLength
    private int height=1;
    private Vector2Int position;

    private int gCost;
    private int hCost;
    private int fCost;
    public GridData comeFromNode;




    public int Num { get => num; set => num = value; }
    // Judge by placeable OBJ?
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
    public IPlaceableObj PlaceableObj { get => placeableObj; set => placeableObj = value; }
    public List<Cube> Cube { get => cube; set => cube = value; }
    public int GCost { get => gCost; set => gCost = value; }
    public int HCost { get => hCost; set => hCost = value; }
    public int FCost { get => fCost; set => fCost = value; }
    public int Height { get => height; set => height = value; }
    public Vector2Int Position { get => position; set => position = value; }

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
        this.PlaceableObj = placeableObj;
        this.height = height;
        this.Position = position;
    }

    public GridData(int num, Vector2Int position, int height, CubeType cubeType)
    {
        this.Num = num;
        this.PlaceableObj = placeableObj;
        this.height = height;
        cube = new List<Cube>();
        this.Position = position;

        var worldPosition = new Vector3(position.x * GridSystem.sideLength, height * GridSystem.sideLength, position.y * GridSystem.sideLength) + GridSystem.origin;

        for (int i = 0; i < height; i++)
        {
            cube.Add(new Cube(CubeType.Base, worldPosition));
        }
        cube.Add(new Cube(cubeType, worldPosition));
    }

    public GridData(GridSystem gs, int num, Vector2Int position, IPlaceableObj placeableObj, int height, CubeType cubeType)
    {
        this.Num = num;
        this.PlaceableObj = placeableObj;
        this.height = height;
        cube = new List<Cube>();
        this.Position = position;

        var worldPosition = new Vector3(position.x * GridSystem.sideLength, height * GridSystem.sideLength, position.y * GridSystem.sideLength) + GridSystem.origin;

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

    CubeType cubeType;
    GameObject prefab;
    GameObject instance;
    public Cube(CubeType cubeType, Vector3 worldPosition)
    {
        var parent = GameObject.Find("Cubes");
        this.cubeType = cubeType; 
        prefab = Resources.Load<GameObject>("Map/" + cubeType.ToString());
        if (!prefab) Debug.LogError("Fail to load resource:" + cubeType.ToString());
        instance = GameObject.Instantiate(prefab, worldPosition, Quaternion.identity, parent.transform);
    }


 }