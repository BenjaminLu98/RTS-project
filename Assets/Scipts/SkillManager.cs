using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillManager
{
    [SerializeField]
    protected SkillUIData uIData;
    public SkillUIData UIData { get { return uIData; } }

    public abstract void RegisterUICallback(SkillUIManager manager);

    public abstract void  UseSkill(int index);
}
