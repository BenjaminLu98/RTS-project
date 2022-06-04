using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class SectorTargetSelector : TargetSelector
{
    private float radius;
    private float degree;

    public SectorTargetSelector(int teamNo, float radius, float degree)
    {
        this.TeamNo = teamNo;
        this.radius = radius;
        this.degree = degree;
    }
    
    // Return the cloest target that is in the target list.
    // If the targetList is empty, this method will return null;
    public override Unit getTarget(int x, int z, IUnit.dir dir)
    {
        List<Unit> targetList = getTargets(x, z, dir);

        float smallestDistance = float.MaxValue;
        Unit smallestUnit = null;
        foreach(Unit unit in targetList)
        {
            float distance = GridUtils.calculateDistance(unit.Position.x, unit.Position.y, x, z);
            if (distance < smallestDistance)
            {
                smallestUnit = unit;
                smallestDistance = distance;
            }
        }
        return smallestUnit;
    }

    // for test
    public override Unit getTarget(Unit unit)
    {
        return unit;
    }

    // Return targets that are in the opposite team, within the radius and the angle from direction < degree/2.
    public override List<Unit> getTargets(int x, int z, IUnit.dir dir)
    {
        List<Unit> targetList = new List<Unit>();

        

        // If the unit is in the opposite team, within the radius and the angle from direction < degree/2, add it to the return list. 
        foreach (Unit unit in Unit.unitList)
        {
            
            if(unit.TeamNo != TeamNo&& GridUtils.calculateDistance(unit.Position.x,unit.Position.y, x, z) <= radius)
            {
                Vector3 dirVector3 = UnitUtil.getDirVector(dir);
                Vector2 dirVector2 = new Vector2(dirVector3.x, dirVector3.z);
                float degreeThreshold = Vector2.Angle(dirVector2, new Vector2(unit.Position.x, unit.Position.y) - new Vector2(x, z));
                if (degree/2 >= degreeThreshold)
                {
                    targetList.Add(unit);
                }
            }
        }
        return targetList;
    }
}
