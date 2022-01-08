using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGridSystem : MonoBehaviour
{
    [SerializeField] public Building keep;
    [SerializeField] public Building keep2;
    [SerializeField] public Archer archer;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(x.ToString()+"  "+y.ToString());
        GridSystem.current.InitializeGridVal();
    }

    // Update is called once per frame
    void Update()
    {
        GridSystem.current.drawDebugLine();
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 position = GridUtils.ScreenToGridPlane(gridSystem);
            //keep.placeAt(position);

            //GridSystem.current.UpdateGridVal();
            //archer.placeAt(position);

            archer.MoveTo(5, 5, 3f);
        }
        if (Input.GetMouseButtonDown(1))
        {

            archer.MoveTo(2, 8, 3f);
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

    private void LateUpdate()
    {
        
    }
}
