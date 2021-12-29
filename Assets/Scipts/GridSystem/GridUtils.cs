using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridUtils 
{
    public static Vector3 ScreenToGridPlane(GridSystem gridSystem)
    {
        Plane plane = new Plane(Vector3.up, gridSystem.origin);
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
