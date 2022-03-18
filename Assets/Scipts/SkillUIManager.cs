using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
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
        var skillImages=new Sprite[8];
        skillImages[0] = uiData.skill1;
        skillImages[1] = uiData.skill2;
        skillImages[2] = uiData.skill3;
        skillImages[3] = uiData.skill4;
        skillImages[4] = uiData.skill5;
        skillImages[5] = uiData.skill6;
        skillImages[6] = uiData.skill7;
        skillImages[7] = uiData.skill8;

        for(var i = 0; i < 8; i++)
        {
            if(skillImages[i] != null)
            {
                CreateSkillImage(skillImages, i);
            }
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
