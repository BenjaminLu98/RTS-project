using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// SkillUIManager control the skill UI and manage .
/// </summary>
public class SkillUIManager : MonoBehaviour
{
   public const int MAX_SKILL_NUM = 8;

    Image[] SkIllSlotUI;
    List<GameObject> addedImgObj;


    private void Start()
    {
        SkIllSlotUI = gameObject.GetComponentsInChildren<Image>();
        if (SkIllSlotUI.Length != 8)
        {
            Debug.LogError($"Invalid skill number{SkIllSlotUI.Length}");
        }
        addedImgObj = new List<GameObject>();
    }

    public void refreshSkillUI(SkillUIData uiData)
    {
        if (uiData == null) return;
        for(var i = 0; i < MAX_SKILL_NUM; i++)
        {
            if (i < uiData.skills.Length)
            {
                if (uiData.skills[i] != null)
                {
                    CreateSkillImage(uiData.skills, i);
                }
            }
            else break;
        }
    }

    private void CreateSkillImage(Sprite[] skillImages, int i)
    {
        var skillImageObj = new GameObject();
        skillImageObj.transform.SetParent(SkIllSlotUI[i].transform);
        skillImageObj.name = i.ToString();

        var rect = skillImageObj.AddComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);
        rect.localScale = Vector2.one;

        var addedImage = skillImageObj.AddComponent<Image>();
        addedImage.sprite = skillImages[i];

        var button = skillImageObj.AddComponent<Button>();

        addedImgObj.Add(skillImageObj);
    }

    public void removeSkillUI()
    {
        foreach(var e in addedImgObj)
        {
            if(e != null)
            {
                Destroy(e);
            }
           
        }
    }

    public void RegisterClickCallback(int index, UnityAction<int> callback, int arg)
    {
        if (callback == null) return;
        if(index < MAX_SKILL_NUM && index>=0)
        {
            var selectedObj = SkIllSlotUI[index].gameObject;
            if (selectedObj.transform.childCount != 0 && selectedObj.GetComponentInChildren<Button>())
            {
                selectedObj.GetComponentInChildren<Button>().onClick.AddListener(() => { callback(arg);});
            }
        }
        else
        {
            Debug.LogWarning($"failed to set skill button callback for index:{index}");
        }
    }
}
