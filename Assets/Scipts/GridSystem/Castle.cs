using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Building
{
    public Castle()
    {
        width = 4;
        height = 4;
    }

    private void Start()
    {
        trainableUnits = new List<GameObject>();
        ResourceLoader rl = FindObjectOfType<ResourceLoader>();
        //Archer
        trainableUnits.Add(rl.resourceList[0]);
    }
}
