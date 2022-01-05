using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGridSystem : MonoBehaviour
{
    GridSystem gridSystem;
    [SerializeField] public Building2m2 keep;
    [SerializeField] public Building2m2 keep2;
    [SerializeField] public Archer archer;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = new GridSystem(transform.position);
        //Debug.Log(gridSystem.getWorldPosition(10, 0));
        int x; int y;
        gridSystem.getXZ(new Vector3(10.5f, 0.0f, 4.5f),out x,out y);
        //Debug.Log(x.ToString()+"  "+y.ToString());
        gridSystem.InitializeGridVal();
        keep.GridSystem = gridSystem;
        keep2.GridSystem = gridSystem;
        archer.GridSystem = gridSystem;

        
    }

    // Update is called once per frame
    void Update()
    {
        gridSystem.drawDebugLine();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GridUtils.ScreenToGridPlane(gridSystem);
            keep.placeAt(position);
            gridSystem.UpdateGridVal();

            //archer.placeAt(position);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            archer.Animator.SetBool("isRunning", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            archer.Animator.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            keep.produce(0);
        }
    }
}
