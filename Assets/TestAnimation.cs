using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    public Unit unit;
    
    public void attack()
    {
        unit.DealDamage(IUnit.DamageType.Physical);
    }
}
