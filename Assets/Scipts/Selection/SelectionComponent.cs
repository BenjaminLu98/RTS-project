using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionComponent  : MonoBehaviour
{
    SkillUIManager skillUIManager;
    /// <summary>
    /// This effect appears when the Unit or Building is selected.
    /// </summary>
    public GameObject selectionEffectObj;
    

    // Start is called before the first frame update
    void OnEnable()
    {
        skillUIManager = GameObject.FindObjectOfType<SkillUIManager>();

        if(selectionEffectObj != null)selectionEffectObj.SetActive(true);

        if (skillUIManager == null) Debug.LogError("Skill manager not found!");

        switch (gameObject.tag)
        {
            case "Building":
                var building = GetComponent<Building>();
                if (building != null)
                {
                    skillUIManager.refreshSkillUI(building.SkillUIData);
                    building.registerSkillUI(skillUIManager);
                }
                break;
            case "Unit":
                break;
        }

    }

    private void OnDisable()
    {
        if(selectionEffectObj != null)selectionEffectObj.SetActive(false);
        if (skillUIManager != null)
        {
            skillUIManager.removeSkillUI();
        }
    }


    private void Update()
    {
        switch (gameObject.tag){
            case "Building":
                break;
            case "Unit":
                if (Input.GetMouseButtonUp(1))
                {
                    Vector3 targetPosition = GridUtils.ScreenToGridPlane();
                    GetComponent<IMoveable>().moveTo(targetPosition, 3f);
                }
                break;
        }
    }
}
