using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyInfantry : Unit
{
    protected void Awake()
    {
        base.Awake();
        width = 1;
        height = 1;
        
        // TODO: define ts.

    }

    protected void Start()
    {
        ts = new ForwardLineSelector(1,defaultCombatData.attackRange);
    }
}
