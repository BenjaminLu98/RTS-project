using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Building
{
    private void Awake()
    {
        base.Awake();
        skillManager.PositionInfo.width = 4;
        skillManager.PositionInfo.height = 4;
    }
    public override void registerSkillUI(SkillUIManager UIManager)
    {
        skillManager.RegisterUICallback(UIManager);
    }

}
