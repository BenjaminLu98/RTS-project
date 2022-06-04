using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : Unit
{
    protected void Awake()
    {
        base.Awake();
        positionInfo.width = 1;
        positionInfo.height = 1;
        
        
    }

    protected void Start()
    {
        
        ts = new SectorTargetSelector(TeamNo, defaultCombatData.attackRange, 128f);
    }
}
