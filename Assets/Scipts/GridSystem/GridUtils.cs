using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridUtils 
{

    /// <summary>
    /// Convert the mouse position from screen position to the world Position on the GridPlane
    /// Note that the grid plane is now fixed to the plane containing the origin and (0,1,0) as normal.
    /// </summary>
    /// <param name="gridSystem"></param>
    /// <returns></returns>
    public static Vector3 ScreenToGridPlane()
    {
        Plane plane = new Plane(Vector3.up, GridSystem.current.origin);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;
        if (plane.Raycast(ray, out distanceToPlane))
        {
            return ray.GetPoint(distanceToPlane);
        }
        else
        {
            Debug.LogError("not on plane");
            return Vector3.zero;
        }
       
    }

    /// <summary>
    /// Using TMP to create text mesh.
    /// </summary>
    /// <param name="words">text content</param>
    /// <param name="location">world space coordinate of text mesh</param>
    /// <param name="parentName">the parent game object's name</param>
    /// <param name="objName">object name</param>
    /// <param name="width">the width of the mesh</param>
    /// <param name="height">the height of the mesh</param>
    /// <param name="fontSize"></param>
    public static void createWorldText(string words, Vector3 location,string parentName, string objName, float width, float height, float fontSize)
    {
        GameObject gameObject = new GameObject(objName, typeof(TextMeshPro));
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        GameObject parent = GameObject.Find(parentName);
        if (!parent){
            parent = new GameObject(parentName);
            parent.transform.position = Vector3.zero;
        }
        gameObject.transform.SetParent(parent.transform);
        var textComponent = gameObject.GetComponent<TextMeshPro>();
        textComponent.text = words;
        textComponent.transform.position = location;
        textComponent.fontSize = fontSize;
        textComponent.alignment = TextAlignmentOptions.Center;
    }

    /// <summary>
    /// updating existing TextMeshPro object
    /// </summary>
    /// <param name="newWords">new content of the text mesh</param>
    /// <param name="parentName">the parent's name</param>
    /// <param name="childName">the gameobject's name</param>
    public static void updateWorldText(string newWords,string parentName, string childName)
    {
        var parent = GameObject.Find(parentName);
        if (parent)
        {
            var child = parent.transform.Find(childName);
            if (child)
            {
                var component = child.GetComponent<TextMeshPro>();
                component.text = newWords;
            }
        }
    }

    /// <summary>
    /// get the 4 neibour nodes at the top, bottom, left, right of the node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static List<GridData> GetNeighbourList(GridData node)
    {
        List<GridData> list = new List<GridData>(); 
        var position = node.Position;
        var x = position.x;
        var y = position.y;
        var width = GridSystem.current.width;
        var height = GridSystem.current.height;
        // left side
        if (x > 0)
        {
            list.Add(GridSystem.current.getGridData(x - 1, y));
            //if (y > 0) list.Add(GridSystem.current.getGridData(x - 1, y - 1));
            //if (y < width - 1) list.Add(GridSystem.current.getGridData(x - 1, y + 1));
        }
        // right side
        if (x < width - 1)
        {
            list.Add(GridSystem.current.getGridData(x + 1, y));
            //if (y > 0) list.Add(GridSystem.current.getGridData(x + 1, y - 1));
            //if (y < width - 1) list.Add(GridSystem.current.getGridData(x + 1, y + 1));
        }
        //bottom
        if (y > 0) list.Add(GridSystem.current.getGridData(x, y - 1));
        //top
        if (y < width - 1) list.Add(GridSystem.current.getGridData(x, y + 1));

        return list;
    }

    public static List<GridData> GetOccupied()
    {
        var list = new List<GridData>();
        for (int i = 0; i < GridSystem.current.width; i++)
        {
            for (int j = 0; j < GridSystem.current.height; j++)
            {
                if (GridSystem.current.getGridData(i, j).IsOccupied)
                {
                    list.Add(GridSystem.current.getGridData(i, j));
                }
            }
        }
        return list;
    }

    public static List<GridData> GetEmptyNeighbourList(GridData node)
    {
        List<GridData> list = new List<GridData>();
        var position = node.Position;
        var x = position.x;
        var y = position.y;
        var width = GridSystem.current.width;
        var height = GridSystem.current.height;
        // left side
        if (x > 0)
        {
            var neibour = GridSystem.current.getGridData(x - 1, y);
            if (!neibour.IsOccupied){
                list.Add(neibour);
            }
            
            //if (y > 0) list.Add(GridSystem.current.getGridData(x - 1, y - 1));
            //if (y < width - 1) list.Add(GridSystem.current.getGridData(x - 1, y + 1));
        }
        // right side
        if (x < width - 1)
        {
            var neibour = GridSystem.current.getGridData(x + 1, y);
            if (!neibour.IsOccupied)
            {
                list.Add(neibour);
            }
            //if (y > 0) list.Add(GridSystem.current.getGridData(x + 1, y - 1));
            //if (y < width - 1) list.Add(GridSystem.current.getGridData(x + 1, y + 1));
        }
        //bottom
        if (y > 0)
        {
            var neibour = GridSystem.current.getGridData(x , y - 1);
            if (!neibour.IsOccupied)
            {
                list.Add(neibour);
            }
        }
        //top
        if (y < width - 1)
        {
            var neibour = GridSystem.current.getGridData(x , y + 1);
            if (!neibour.IsOccupied)
            {
                list.Add(neibour);
            }
        }

        return list;
    }

    public static float calculateDistance(int x1, int z1, int x2, int z2)
    {
       return Vector2Int.Distance(new Vector2Int(x1, z1), new Vector2Int(x2, z2))*GridSystem.current.sideLength;
    }

}
