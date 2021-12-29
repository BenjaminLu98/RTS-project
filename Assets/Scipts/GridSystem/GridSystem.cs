using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    public Vector3 origin;
    public int width = 10;
    public int height = 10;
    public float sideLength = 2f;
    GridData[,] gridDataArray;

    public GridSystem(Vector3 origin)
    {
        this.origin = origin;
        gridDataArray = new GridData[width, height];
        for(int i=0; i < width; i++)
        {
            for(int j = 0; j < height; j++){
                gridDataArray[i, j] = new GridData();
            }
        }
    }

    //Get the world position of grid(x,z). x is [0,width), z is [0,heihgt)
    public Vector3 getWorldPosition(int x, int z) {
        if (x > width)
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": x({x}) out of range! ");
        }
        if (z > height)
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": z({z}) out of range! ");
        }
        return new Vector3(x * sideLength, z * sideLength, 0f) + origin;
    }

    //Get gridSystem position(x,y) from world Position. The position should be on the grid System plane.
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

    // Draw grid data using TextmeshPro. Do not use it in Update()!
    public void InitializeGridVal() {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridUtils.createWorldText(gridDataArray[i, j].num.ToString(), new Vector3(origin.x + i * sideLength+0.5f*sideLength, origin.y, origin.z + j * sideLength + 0.5f * sideLength), "GridTexts", "GridVal"+i+"*"+j,1f ,1f, 7f);
            }
        }
        
    }

    //Update grid value each frame. Call it in Update
    public void UpdateGridVal()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridUtils.updateWorldText(gridDataArray[i, j].num.ToString(), "GridTexts", "GridVal" + i + "*" + j);
            }
        }
    }
    
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

    public void setValue(int x, int z, GridData data)
    {
        gridDataArray[x, z] = data;
    }

    public void setValue(Vector3 worldPosition, GridData data)
    {
        if (checkWorldPosition(worldPosition))
        {
            int x; int z;
            getXZ(worldPosition, out x,out z);
            gridDataArray[x, z] = data;
        } 
    }

    bool checkWorldPosition(Vector3 worldPosition)
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
}
