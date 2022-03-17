using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMap
{
    public Dictionary<int, GameObject> selectionMap=new Dictionary<int, GameObject>();
    string currentTag;

    //TODO: select building
    /// <summary>
    /// Add the obj into the map and add a Selection component to this object.
    /// </summary>
    /// <param name="obj"></param>
    public void add(GameObject obj)
    {
        if (obj.tag != currentTag&&selectionMap.Count!=0)
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
                obj.AddComponent<SelectionComponent>();
                Debug.Log("Added " + id + " to selected dict");
            }
        }
    }

    public void remove(GameObject obj)
    {
        if (obj != null)
        {
            GameObject.DestroyImmediate(obj.GetComponent <SelectionComponent>());
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
                GameObject.DestroyImmediate(pair.Value.GetComponent<SelectionComponent>());
                Debug.Log("Removed " + pair.Value.GetInstanceID() + " to selected dict");
            }
   
        }
        selectionMap.Clear();
    }
}
