using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Grid;

[CustomEditor(typeof(ResourceLoader))]
public class DetailEditor : Editor
{
    SerializedProperty rl;
    GameObject currentActivePrefab;
    bool isDrawing;
    SceneView view;

    private void OnEnable()
    {
        rl = serializedObject.FindProperty("resourceList");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        EditorGUILayout.PropertyField(rl);
        serializedObject.ApplyModifiedProperties();
        
        view = GetSceneView();
        view.Focus();
    }

    private void OnSceneGUI()
    {
        // Press R to change drawing state. 1~5 to change the object to be painted.
        if (Event.current.type == EventType.KeyDown)
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.Alpha1:
                    currentActivePrefab = rl.GetArrayElementAtIndex(0).objectReferenceValue as GameObject;
                    Debug.Log("No.1 object selected");
                    break;
                case KeyCode.Alpha2:
                    currentActivePrefab = rl.GetArrayElementAtIndex(1).objectReferenceValue as GameObject;
                    Debug.Log("No.2 object selected");
                    break;
                case KeyCode.Alpha3:
                    currentActivePrefab = rl.GetArrayElementAtIndex(2).objectReferenceValue as GameObject;
                    Debug.Log("No.3 object selected");
                    break;
                case KeyCode.Alpha4:
                    currentActivePrefab = rl.GetArrayElementAtIndex(3).objectReferenceValue as GameObject;
                    Debug.Log("No.4 object selected");
                    break;
                case KeyCode.Alpha5:
                    currentActivePrefab = rl.GetArrayElementAtIndex(4).objectReferenceValue as GameObject;
                    Debug.Log("No.5 object selected");
                    break;
                case KeyCode.R:
                    isDrawing = !isDrawing;
                    Debug.Log("Drawing mode:" + isDrawing);
                    break;
            }
            if (currentActivePrefab == null)
            {
                Debug.LogError("resource loader is not long enough");
            }
        }

        if (isDrawing)
        {
            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Plane plane = new Plane(Vector3.up, GridSystem.current.origin);
            float dist;
            plane.Raycast(ray, out dist);
            int x;int z;
            GridSystem.current.getXZ(ray.GetPoint(dist), out x, out z);
            Debug.Log($"x:{x},z:{z}");
            if(GridSystem.current.checkOccupation(x, z))
            {
                var mapObj = GameObject.Find("Map");
                var obj = GameObject.Instantiate(currentActivePrefab, GridSystem.current.getWorldPosition(x, z), Quaternion.identity, mapObj.transform);
                var placeable = obj.GetComponent<IPlaceableObj>();
                placeable.placeAt(x, z);
            }
        }
    }

    public static SceneView GetSceneView()
    {
        SceneView view = SceneView.lastActiveSceneView;
        if(view == null)
        {
            view = EditorWindow.GetWindow<SceneView>();
        }
        return view;
    }

}
