using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GridPreviewEditor : EditorWindow
{
    [SerializeField]
    public List<GameObject> prefabList;
    private int width;
    private int height;
    private float sideLength;
    private string dataPath;

    // The entrance of this editor
    [MenuItem("MapGenrator/GridPreviewEditor")]
    static void run()
    {
        EditorWindow.CreateWindow<GridPreviewEditor>();
    }

    //
    public void OnGUI()
    {
        GUILayout.Label("width");
        width = int.Parse(GUILayout.TextField(width.ToString()));
        GUILayout.Label("height");
        height = int.Parse(GUILayout.TextField(height.ToString()));
        GUILayout.Label("side length");
        sideLength = float.Parse(GUILayout.TextField(sideLength.ToString()));
        
        if (Selection.activeGameObject != null)
        {
            GUILayout.Label(Selection.activeGameObject.name);
            if(GUILayout.Button("generate grid priview"))
            {
                GridSystem.current.clearArray();
                GridSystem.current.Initialize(Selection.activeGameObject.transform.position,width,height);
            }

            // Note that this button assume that we have selected the map corresponding to the loading GridSystem.
            // TODO: only load to object named "Map".
            if (GUILayout.Button("load the gridSystem state of the map object"))
            {
                var selectedPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(Selection.activeGameObject);

                string jsonText = File.ReadAllText(Application.dataPath + "/RTS_data/Map.json");

                GridSystem.current.fromJson(jsonText);

                Selection.activeGameObject.transform.position = GridSystem.current.origin;
            }

            // This button will override the prefab if Map.prefab already exists.
            if (GUILayout.Button("save preview and GridSystemState"))
            {
                string localPath = "Assets/Resources/Map/MapPrefabs/Map.prefab";
                //localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                string jsonPath = Application.dataPath + "/RTS_data/Map.json";

                var mapObj = GameObject.Find("Map");
                if (mapObj)
                {
                    File.WriteAllText(jsonPath, GridSystem.current.toJson());
                    PrefabUtility.SaveAsPrefabAssetAndConnect(mapObj, localPath, InteractionMode.UserAction);
                }
                else
                {
                    Debug.LogWarning("Save prefab not successed. No map obj found.");
                }
            }
        }

        
    }

    private void OnSelectionChange()
    {
        this.Repaint();
    }
}
