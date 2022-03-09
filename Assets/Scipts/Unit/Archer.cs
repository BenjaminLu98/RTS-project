using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    private void Awake()
    {
        base.Awake();
        width = 1;
        height = 1;
        animator = transform.GetChild(0).GetComponent<Animator>();
        // TODO: define ts.
        
    }

    private void Start()
    {
        base.Start();
        ts = new SectorTargetSelector(TeamNo, 3, 128f);
    }
}
