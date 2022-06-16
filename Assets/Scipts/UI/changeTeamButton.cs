using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeTeamButton : MonoBehaviour
{
    public GameObject mouseOperationObj;
    MouseOperation mouseOperation;
    List<GameObject> selectedObjs;

    private void Start()
    {
        if (mouseOperationObj != null)
        {
            mouseOperation=mouseOperationObj.GetComponent<MouseOperation>();
        }
    }
    public void changeTeam()
    {
        selectedObjs = mouseOperation.getSelctedObjects();
        if (selectedObjs.Count > 0)
        {
            if (selectedObjs[0].tag == "Unit")
            {
                foreach (GameObject obj in selectedObjs)
                {
                    obj.GetComponent<Unit>().changeTeamNo();
                }
            }
        }
    }
}
