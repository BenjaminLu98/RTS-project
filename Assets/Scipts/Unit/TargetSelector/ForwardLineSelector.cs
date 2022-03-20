using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ForwardLineSelector : TargetSelector
{
    int attackRange;
    public ForwardLineSelector(int teamNo, int attackRange)
    {
        this.TeamNo = teamNo;
        this.attackRange = attackRange;
    }

    public override Unit getTarget(int x, int z, IUnit.dir dir)
    {
        for(int i = 0; i < attackRange; i++)
        {
            var attactVec = UnitUtil.getDirVector2D(dir) * (i + 1);
            foreach (var unit in Unit.unitList)
            {
                if(unit.teamNo != TeamNo)
                {
                    var targetPos = attactVec + new Vector2Int(x, z);
                    if (targetPos == unit.Position)
                    {
                        return unit;
                    }
                }
            }
        }
        return null;
            
    }

    public override Unit getTarget(Unit unit)
    {
        return unit;
    }

    /// <summary>
    /// get targets that is in the attack range. 
    /// </summary>
    /// <returns>if no targets, return empty list.</returns>
    public override List<Unit> getTargets(int x, int z, IUnit.dir dir)
    {
        List<Unit> targets = new List<Unit>();
        foreach (var unit in Unit.unitList)
        {
            if (unit.teamNo != TeamNo)
            {
                for (int j = 0; j < attackRange; j++)
                {
               
                    var attactVec = UnitUtil.getDirVector2D(dir) * (j + 1);
                    var targetPos = attactVec + new Vector2Int(x, z);
                    if (targetPos == unit.Position)
                    {
                        targets.Add(unit);
                        break;
                    }
                }
            }
        }
        return targets;

    }
}

