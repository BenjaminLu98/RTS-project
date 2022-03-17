using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionComponent  : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    private void Update()
    {
        switch (gameObject.tag){
            case "Building":
                break;
            case "Unit":
                if (Input.GetMouseButtonUp(1))
                {
                    Vector3 targetPosition = GridUtils.ScreenToGridPlane();
                    GetComponent<IMoveable>().moveTo(targetPosition, 3f);
                }
                break;
        }

        
    }
}
