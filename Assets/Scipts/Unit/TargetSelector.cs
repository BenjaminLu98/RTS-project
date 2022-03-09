using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetSelector
{
    protected int teamNo;

    abstract public List<Unit> getTargets(int x, int z, IUnit.dir dir);

    abstract public Unit getTarget(int x, int z, IUnit.dir dir);

    abstract public Unit getTarget(Unit unit );

}
