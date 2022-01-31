using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionComponent : MonoBehaviour
{
    Unit unit;
    // Start is called before the first frame update
    void OnEnable()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 targetPosition = GridUtils.ScreenToGridPlane();
            unit.moveTo(targetPosition, 3f);
        }
        
    }


}
