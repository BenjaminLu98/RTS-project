using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyInfantry : Unit
{
    protected void Awake()
    {
        base.Awake();
        positionInfo.width = 1;
        positionInfo.height = 1;
        
        // TODO: define ts.

    }

    protected void Start()
    {
        ts = new ForwardLineSelector(teamNo,defaultCombatData.attackRange);
    }
}
