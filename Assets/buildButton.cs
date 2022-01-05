using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class buildButton : MonoBehaviour 
{ 
    Button button;
    public GameObject previewPrefab;
    GameObject previewInstance;
    public GameObject prefab;
    GameObject instance;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(createPreview);
    }

    void  createPreview()
    {
        if (!previewPrefab)
        {
            Debug.LogError(GetType().Name + " :preview not loaded!");
            return;
        }
        previewInstance = Instantiate(previewPrefab, GridUtils.ScreenToGridPlane(GridSystem.gridSystem), Quaternion.identity);
    }

    void destroyPreview()
    {
        Destroy(previewInstance);
        previewInstance = null;
    }

    private void Update()
    {
        if (previewInstance)
        {
            Vector3 worldPosition= GridUtils.ScreenToGridPlane(GridSystem.current);
            int x, z;
            GridSystem.current.getXZ(worldPosition, out x, out z);
            previewInstance.transform.position = GridSystem.current.getWorldPosition(x,z);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GridUtils.ScreenToGridPlane(GridSystem.current);
            bool result = GridSystem.current.checkWorldPosition(position);
            if(result)
            {
                instance = Instantiate(prefab, previewInstance.transform.position, Quaternion.identity);
                instance.GetComponent<IPlaceableObj>().placeAt(previewInstance.transform.position);
                switch (previewInstance.GetComponent<IBuilding>().CurrentDir)
                {
                    case IBuilding.dir.backward:
                        break;
                    case IBuilding.dir.right:
                        instance.GetComponent<IBuilding>().rotate();
                        break;
                    case IBuilding.dir.forward:
                        instance.GetComponent<IBuilding>().rotate();
                        instance.GetComponent<IBuilding>().rotate();
                        break;
                    case IBuilding.dir.left:
                        instance.GetComponent<IBuilding>().rotate();
                        instance.GetComponent<IBuilding>().rotate();
                        instance.GetComponent<IBuilding>().rotate();
                        break;
                }
                if (previewInstance)
                {
                    destroyPreview();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (previewInstance)
            {
                previewInstance.GetComponent<IBuilding>().rotate();
            }
        }
    }
}
