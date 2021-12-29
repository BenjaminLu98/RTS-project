using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGridSystem : MonoBehaviour
{
    GridSystem gridSystem;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = new GridSystem(transform.position);
        //Debug.Log(gridSystem.getWorldPosition(10, 0));
        int x; int y;
        gridSystem.getXZ(new Vector3(10.5f, 0.0f, 4.5f),out x,out y);
        //Debug.Log(x.ToString()+"  "+y.ToString());
        gridSystem.InitializeGridVal();
    }

    // Update is called once per frame
    void Update()
    {
        gridSystem.drawDebugLine();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GridUtils.ScreenToGridPlane(gridSystem);
            gridSystem.setValue(position, new GridData(10));
            gridSystem.UpdateGridVal();
        }
    }
}
