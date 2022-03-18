using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    const int MAX_SKILL_NUM = 8;

    Image[] SkIllSlotUI;
    List<Image> addedSkillUI;


    private void Start()
    {
        SkIllSlotUI = gameObject.GetComponentsInChildren<Image>();
        if (SkIllSlotUI.Length != 8)
        {
            Debug.LogError($"Invalid skill number{SkIllSlotUI.Length}");
        }
        addedSkillUI = new List<Image>();
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

        addedSkillUI.Add(addedImage);
    }

    public void removeSkillUI()
    {
        foreach(var e in addedSkillUI)
        {
            if(e != null)
            {
                Destroy(e.gameObject);
            }
           
        }
    }
}
