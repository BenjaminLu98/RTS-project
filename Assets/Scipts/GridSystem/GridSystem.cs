using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GridSystem
{
    public Vector3 origin;
    public int width = 100;
    public int height = 100;
    public float sideLength = 1f;
    GridData[,] gridDataArray;

    public static GridSystem gridSystem;
    public static GridSystem current
    {
        get
        {
            if(gridSystem == null)
            {
                string jsonText = File.ReadAllText(Application.persistentDataPath + "/RTS_data/Map.prefab.json");
                gridSystem = new GridSystem();
                GridSystem.gridSystem.fromJson(jsonText);
                if (GridSystem.Initialized == false) Debug.LogError("GridSystem not initialized properly");
            }
            return gridSystem;
        }
    }

    public static bool Initialized
    {
        get
        {
            return !(gridSystem.gridDataArray == null);
        }
    }

    public string toJson()
    {
        GridData[] _1dArray = new GridData[width * height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _1dArray[i*height+j] = gridDataArray[i, j];
            }
        }
        var wrapper = new GridDataWrapper(_1dArray,origin,width,height,sideLength);
        return JsonUtility.ToJson(wrapper);
    }

    public void fromJson(string str)
    {
        var wrapper = JsonUtility.FromJson<GridDataWrapper>(str);
        origin = wrapper.origin;
        width = wrapper.width;
        height = wrapper.height;
        sideLength = wrapper.sideLength;
        gridDataArray = new GridData[width, height];
        for(int i = 0; i < wrapper.gridDataArray.Length; i++)
        {
            gridDataArray[i / height, i % height] = wrapper.gridDataArray[i];
        }
    }

    public void Initialzation()
    {
        origin = new Vector3(0, 0, 0);
        gridDataArray = new GridData[width, height];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                gridDataArray[i, j] = new GridData(0, new Vector2Int(i, j), 0, CubeType.Grass);
            }
        }
    }

    public void clearArrayAndCubes()
    {
        if (gridDataArray == null) return;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                gridDataArray[i, j].Cube.ForEach((e)=>e.DestroyInstance());
            }
        }
        gridDataArray = null;
    }

    public void Initialzation(Vector3 origin, int width, int height)
    {
        GridSystem.current.origin = origin;
        GridSystem.current.width = width;
        GridSystem.current.height = height;
        gridDataArray = new GridData[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                gridDataArray[i, j] = new GridData(0, new Vector2Int(i, j), 0, CubeType.Grass);
            }
        }
    }

    //Get the world position of grid(x,z). x is [0,width), z is [0,heihgt)
    public Vector3 getWorldPosition(int x, int z) {
        if (!checkWidthHeight(x,z))
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": x({x}) z({z}) out of range! ");
        }
        if (getGridData(x, z) != null)
        {
            return new Vector3(x * sideLength, getGridData(x, z).Height * sideLength, z * sideLength) + origin;
        }
        else
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": x({x}) z({z}) data is null! ");
            return new Vector3(0, 0, 0);
        }
        
    }

    /// <summary>
    /// Get gridSystem position(x,y) from world Position. The position should be on the grid System plane.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void getXZ(Vector3 worldPosition, out int x, out int z)
    {
        if (worldPosition.x < origin.x)
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": WorldPosition.x({worldPosition.x}) is lower than origin(${origin.x})");
        }
        if (worldPosition.z < origin.z)
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": WorldPosition.y({worldPosition.z}) is lower than origin(${origin.z})");
        }
        Vector3 diff = worldPosition - origin;
        x = Mathf.FloorToInt(diff.x / sideLength);
        z = Mathf.FloorToInt(diff.z / sideLength);
    }

    public float getY()
    {
        return origin.y;
    }

    /// <summary>
    /// Draw grid data using TextmeshPro. Use it in the start method.
    /// </summary>
    public void InitializeGridVal() {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridUtils.createWorldText(gridDataArray[i, j].Num.ToString(), new Vector3(origin.x + i * sideLength+0.5f*sideLength, origin.y, origin.z + j * sideLength + 0.5f * sideLength), "GridTexts", "GridVal"+i+"*"+j,1f ,1f, 7f);
            }
        }
        
    }

    /// <summary>
    /// Update grid value each frame for debugging. Call it when the grid value need to be update.
    /// </summary>
    public void UpdateGridVal()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridUtils.updateWorldText(gridDataArray[i, j].Num.ToString(), "GridTexts", "GridVal" + i + "*" + j);
            }
        }
    }
    
    // TODO: move to util
    //Draw the grid
    public void drawDebugLine()
    {
       for(int i = 0; i < width; i++)
        {
            for(int j = 0; j<height; j++)
            {
                Debug.DrawLine(new Vector3(origin.x + i * sideLength, origin.y, origin.z + j * sideLength), new Vector3(origin.x + (i + 1) * sideLength, origin.y, origin.z + j * sideLength), Color.red);
                Debug.DrawLine(new Vector3(origin.x + i * sideLength, origin.y, origin.z + j * sideLength), new Vector3(origin.x + i * sideLength, origin.y, origin.z + (j + 1) * sideLength), Color.red);
            }
        }
        Debug.DrawLine(new Vector3(origin.x, origin.y, origin.z + height * sideLength), new Vector3(origin.x + width * sideLength, origin.y, origin.z + height * sideLength), Color.red);
        Debug.DrawLine(new Vector3(origin.x + width * sideLength, origin.y, origin.z), new Vector3(origin.x + width * sideLength, origin.y, origin.z + height * sideLength), Color.red);

    }

    /// <summary>
    /// update the Grid data array with the parameters and mark them as occupied.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="data"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public bool setValue(int x, int z, int num, IPlaceableObj placeable,  int width=1, int height=1)
    {
        if (!checkWidthHeight(x, z, width, height))
        {
            Debug.LogError("no rectangle exists at (x,z) with this set of width and height.");
            return false;
        }
        //TODO: Add UI hint.
        else if (!checkOccupationExcept(x, z, width, height,placeable))
        {
            Debug.LogError("At least one grid is already occupied!");
            return false;
        }
        else
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridDataArray[x + i, z + j].Num = num; 
                    gridDataArray[x + i, z + j].PlaceableObj = placeable;
                    gridDataArray[x + i, z + j].IsOccupied = true;
                }
            }
            return true;
        }
        
    }



    public bool setValue(Vector3 worldPosition, int num, IPlaceableObj placeable, int width = 1, int height = 1)
    {
        if (checkWorldPosition(worldPosition))
        {
            int x; int z;
            getXZ(worldPosition, out x, out z);
            return setValue(x, z, num, placeable, width, height);
        }
        else return false;
    }

    public bool setHeight(int x,int z,int height)
    {
        if (height >= 0)
        {
            if (checkWidthHeight(x, z))
            {
                gridDataArray[x, z].Height = height;
                return true;
            }
        }
        return false;
    }

    public int getHeight(int x, int z)
    {

        if (checkWidthHeight(x, z))
        {
            return gridDataArray[x, z].Height;
        }
        else
        {
            return 0;
        }

    }


    /// <summary>
    /// remove all the grid data within the width*height rectangle at (x,z)
    /// </summary>
    /// <param name="x">grid coordinate x</param>
    /// <param name="z">grid coordinate y</param>
    /// <param name="width">how many grids horizontally in the rect</param>
    /// <param name="height">how many grids vertically in the rect</param>
    public void removeValue(int x, int z, int width = 1, int height = 1)
    {
        if (!checkWidthHeight(x, z, width, height))
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + " :checkWidthHeight failed");
        }
        else
        {
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    gridDataArray[x + i, z + j].Num = 0;
                    gridDataArray[x + i, z + j].IsOccupied = false;
                    gridDataArray[x + i, z + j].PlaceableObj = null;
                }
            }
        }
    }

    public void removeValue(Vector3 worldPosition, int width = 1, int height = 1)
    {
        int x, z;
        getXZ(worldPosition, out x, out z);
        removeValue(x, z, width, height);
    }

    /// <summary>
    /// check if the given world position is within the boundary
    /// x:[origin.x, origin.x + width*sideLength] 
    /// y:[origin.y, origin.y + height*sideLength]
    /// </summary>
    /// <param name="worldPosition">World position to be checked</param>
    /// <returns>true if it is within the range</returns>
    public bool checkWorldPosition(Vector3 worldPosition)
    {
        if (worldPosition.x >= origin.x && worldPosition.x <= origin.x + width * sideLength)
        {
            if(worldPosition.z >= origin.z && worldPosition.z <= origin.z + height * sideLength)
            {
                if (worldPosition.y == origin.y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// check if the width*height grid rectangle with its left-bottom point at grid position(x,z) exists in current grid system.
    /// </summary>
    /// <param name="x">grid coordinate x</param>
    /// <param name="z">grid coordinate y</param>
    /// <param name="width">how many grids horizontally in the rect</param>
    /// <param name="height">how many grids vertically in the rect</param>
    /// <returns></returns>
    public bool checkWidthHeight(int x, int z, int width = 1, int height = 1)
    {
        if (x >= 0 && z >= 0 && x + width <= GridSystem.current.width && z + height <= GridSystem.current.height) return true;
        else return false;
    }

    /// <summary>
    /// check if the width*height rectangle at (x,z) is occupied.
    /// </summary>
    /// <param name="x">grid coordinate x</param>
    /// <param name="z">grid coordinate y</param>
    /// <param name="width">how many grids horizontally in the rect</param>
    /// <param name="height">how many grids vertically in the rect</param>
    /// <returns>if all of the rectangle is not occupied, return true. Otherwise return false</returns>
    public bool checkOccupation(int x, int z, int width, int height)
    {
        if (!checkWidthHeight(x, z, width, height)) return false;
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (gridDataArray[x + i, z + j].IsOccupied)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Find a grid that is not occupied around the given rectangle.
    /// </summary>
    /// <param name="gridCoordinate">the origin of the rectangle</param>
    /// <param name="width">the width of the rectangle</param>
    /// <param name="height">the height of the rectangle</param>
    /// <returns>return the rectangle's left-bottom point if it found a blank point. Otherwise it will return (0,0)</returns>
    public Vector2Int getBlankGrid(Vector2Int gridCoordinate, int width, int height)
    {
        //TODO: extend this function so that it can find blank rectangle
        gridCoordinate -= Vector2Int.one;
        width += 2;
        height += 2;

        //check if there is a blank grid backward
        for(int i = 0; i < width; i++)
        {
            if (gridCoordinate.x + i >= 0 && gridCoordinate.x + i < GridSystem.current.width && gridCoordinate.y >= 0 && !gridDataArray[gridCoordinate.x + i, gridCoordinate.y].IsOccupied)
            {
                return new Vector2Int(gridCoordinate.x + i, gridCoordinate.y);
            }
        }

        //check if there is a blank grid left
        for(int j=0;j<height;j++)
        {
            if(gridCoordinate.y + j>=0 && gridCoordinate.y + j < GridSystem.current.height && gridCoordinate.x >=0 && !gridDataArray[gridCoordinate.x, gridCoordinate.y+j].IsOccupied)
            {
                return new Vector2Int(gridCoordinate.x, gridCoordinate.y + j);
            }
        }

        //check if there is a blank grid forward
        for (int i = 0; i < width; i++)
        {
            if (gridCoordinate.x + i >= 0 && gridCoordinate.x + i < GridSystem.current.width && gridCoordinate.y + height - 1  < GridSystem.current.height && !gridDataArray[gridCoordinate.x + i, gridCoordinate.y + height-1].IsOccupied)
            {
                return new Vector2Int(gridCoordinate.x + i, gridCoordinate.y + height -1);
            }
        }

        //check if there is a blank grid right
        for (int j = 0; j < height; j++)
        {
            if (gridCoordinate.y + j >= 0 && gridCoordinate.y + j < GridSystem.current.height && gridCoordinate.x + width -1 < GridSystem.current.width && !gridDataArray[gridCoordinate.x + width -1, gridCoordinate.y + j].IsOccupied)
            {
                return new Vector2Int(gridCoordinate.x + width -1, gridCoordinate.y + j);
            }
        }

        return Vector2Int.zero;

    }

    /// <summary>
    /// check if the grid is occupied
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>true if it is not occupied</returns>
    public bool checkOccupation(int x, int z)
    {
        checkWidthHeight(x, z, 1, 1);
        return !gridDataArray[x, z].IsOccupied;
    }

    /// <summary>
    /// check if the grid is occupied. Ignore obj.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>true if it is not occupied or the occupied grid contains obj.</returns>
    public bool checkOccupationExcept(int x, int z, IPlaceableObj obj)
    {
        checkWidthHeight(x, z, 1, 1);
        return !gridDataArray[x, z].IsOccupied || obj.Equals(gridDataArray[x, z].PlaceableObj);
    }

    public bool checkOccupationExcept(int x, int z, int width, int height, IPlaceableObj obj)
    {

        if (!checkWidthHeight(x, z, width, height)) return false;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (gridDataArray[x + i, z + j].IsOccupied && (obj.Equals(null)||!obj.Equals(gridDataArray[x,z].PlaceableObj)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public GridData getGridData(int x, int z)
    {
        if (!checkWidthHeight(x, z))
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod() + ": (" + x + "," + z + ") is not a valid position in the grid system");
            return null;
        }
        return gridDataArray[x, z];
    }
}
