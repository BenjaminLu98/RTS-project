using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGridSystem : MonoBehaviour
{
    [SerializeField] public Building keep;
    [SerializeField] public Building keep2;
    [SerializeField] public GameObject crossbow;
    void Awake()
    {
        //Debug.Log(x.ToString()+"  "+y.ToString());
        //GridSystem.current.InitializeGridVal();
        //var gs = GridSystem.current;

        var crossbow1OBJ = Instantiate(crossbow);
        var crossBow1 = crossbow1OBJ.GetComponent<Archer>();
        crossBow1.TeamNo = 1;
        crossBow1.placeAt(3, 4);

        var crossbow2OBJ = Instantiate(crossbow);
        var crossBow2 = crossbow2OBJ.GetComponent<Archer>();
        crossBow2.TeamNo = 0;
        crossBow2.placeAt(10, 4);

        var crossbow3OBJ = Instantiate(crossbow);
        var crossBow3 = crossbow3OBJ.GetComponent<Archer>();
        crossBow3.TeamNo = 0;
        crossBow3.placeAt(6, 3);




    }


    // Update is called once per frame
    void Update()
    {
        //GridSystem.current.drawDebugLine();
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 position = GridUtils.ScreenToGridPlane(gridSystem);
            //keep.placeAt(position);

            //GridSystem.current.UpdateGridVal();
            //archer.placeAt(position);

            //archer.MoveTo(5, 5, 3f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //int x, z;
            //GridSystem.current.getXZ(new Vector3(0.5f, 0f, 0.5f), out x,out z);
            //Debug.Log(x + "," + z);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            keep.produce(0);
        }
        //GridSystem.current.UpdateGridVal();
    }

    private void LateUpdate()
    {
        
    }
}
