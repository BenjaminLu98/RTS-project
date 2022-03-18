using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill UI Data", menuName = "Skill UI Data")]
public class SkillUIData: ScriptableObject
{
    [Header("skill image")]
    public Sprite[] skills;
}

