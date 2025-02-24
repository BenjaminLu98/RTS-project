﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMap
{
    private Dictionary<int, GameObject> selectionMap=new Dictionary<int, GameObject>();
    string currentTag;

    /// <summary>
    /// Add the obj into the map and add a Selection component to this object.
    /// </summary>
    /// <param name="obj"></param>
    public void add(GameObject obj)
    {
        if (obj.tag != currentTag && selectionMap.Count!=0)
        {
            removeAll();
        }
        currentTag = obj.tag;

        if (obj != null)
        {
            int id = obj.GetInstanceID();
            if (!(selectionMap.ContainsKey(id)))
            {
                selectionMap.Add(id, obj);
                obj.GetComponent<SelectionComponent>().enabled=true;
                Debug.Log("Added " + id + " to selected dict");
            }
        }
    }

    public void remove(GameObject obj)
    {
        if (obj != null)
        {
            obj.GetComponent<SelectionComponent>().enabled=false;
            selectionMap.Remove(obj.GetInstanceID());
            Debug.Log("Removed " + obj.gameObject.GetInstanceID() + " to selected dict");
        }

    }

    public void removeAll()
    {
        foreach(KeyValuePair<int,GameObject>pair in selectionMap)
        {
            if (pair.Value != null)
            {
                pair.Value.GetComponent<SelectionComponent>().enabled=false;
                Debug.Log("Removed " + pair.Value.GetInstanceID() + " to selected dict");
            }
   
        }
        selectionMap.Clear();
    }

    public List<GameObject> getSelectedObjects()
    {
        List<GameObject> selectedObjects = new List<GameObject>();
        foreach (KeyValuePair<int,GameObject> obj in selectionMap)
        {
            selectedObjects.Add(obj.Value);
        }
        return selectedObjects;
    }
}
