using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDamageEvent:MonoBehaviour
{
    Unit unit;

    private void OnEnable()
    {
        unit = transform.parent.gameObject.GetComponent<Unit>();
        if (unit == null) Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + "parent object dont have Unit component");
    }

    public void attack()
    {
        if (unit == null) return;
        unit.DealDamage(IUnit.DamageType.Physical);
    }
}
