using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    [Serializable]
    internal class UnitSkillManager : SkillManager
    {
        List<Skill> skillList;

        public override void RegisterUICallback(SkillUIManager manager)
        {
            throw new NotImplementedException();
        }

        public override void UseSkill(int index)
        {
            throw new NotImplementedException();
        }
    }
}
