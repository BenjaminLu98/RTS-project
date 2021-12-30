using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    private void Awake()
    {
        width = 1;
        height = 1;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
}
