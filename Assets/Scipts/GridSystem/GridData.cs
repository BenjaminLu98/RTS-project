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
    // Lowest height is 0, normally it is 1.
    private int height;
    private Vector2Int position;

    
    public int Num { get => num; set => num = value; }
    // Judge by placeable OBJ?
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
    public IPlaceableObj PlaceableObj { get => placeableObj; set => placeableObj = value; }
    public List<Cube> Cube { get => cube; set => cube = value; }

    public GridData()
    {
        this.Num = 0;
        position = new Vector2Int(0, 0);
    }

    // Start is called before the first frame update
    public GridData(int num)
    {
        this.Num = num;
        position = new Vector2Int(0, 0);
    }
    public GridData(int num, Vector2Int position, IPlaceableObj placeableObj, int height)
    {
        this.Num = num;
        this.PlaceableObj = placeableObj;
        this.height = height;
        this.position = position;
    }

    public GridData(int num, Vector2Int position, int height, CubeType cubeType)
    {
        this.Num = num;
        this.PlaceableObj = placeableObj;
        this.height = height;
        cube = new List<Cube>();
        this.position = position;
        for (int i = 0; i < height; i++)
        {
            cube.Add(new Cube(CubeType.Base, position));
        }
        cube.Add(new Cube(cubeType, position));
    }

    public GridData(GridSystem gs, int num, Vector2Int position, IPlaceableObj placeableObj, int height, CubeType cubeType)
    {
        this.Num = num;
        this.PlaceableObj = placeableObj;
        this.height = height;
        cube = new List<Cube>();
        this.position = position;
        for (int i = 0; i < height-1; i++)
        {
            cube.Add(new Cube(CubeType.Base, position));
        }
        cube.Add(new Cube(cubeType, position));
    }


}



public class Cube
{

    CubeType cubeType;
    GameObject prefab;
    GameObject instance;
    public Cube(CubeType cubeType, Vector2Int position)
    {
        this.cubeType = cubeType;
        prefab = Resources.Load<GameObject>("Map/" + cubeType.ToString());
        if (!prefab) Debug.LogError("Fail to load resource:" + cubeType.ToString());
        instance = GameObject.Instantiate(prefab, GridSystem.current.getWorldPosition(position.x, position.y), Quaternion.identity);
    }


 }