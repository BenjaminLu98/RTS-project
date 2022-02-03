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
        Plane plane = new Plane(Vector3.up, GridSystem.origin);
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

}
