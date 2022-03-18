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
    public Sprite skill1;
    public Action skill1Callback;
    public Sprite skill2;
    public Sprite skill3;
    public Sprite skill4;
    public Sprite skill5;
    public Sprite skill6;
    public Sprite skill7;
    public Sprite skill8;
}

