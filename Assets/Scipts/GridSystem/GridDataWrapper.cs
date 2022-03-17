
using UnityEngine;

[System.Serializable]
public class GridDataWrapper 
{
    public Vector3 origin;
    public int width;
    public int height;
    public float sideLength;
    public GridData[] gridDataArray;
    public GridDataWrapper(GridData[] array, Vector3 origin, int width, int height, float sideLength)
    {
        gridDataArray = array;
        this.origin = origin;
        this.width = width;
        this.height = height;
        this.sideLength = sideLength;
    }
}
