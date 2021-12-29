using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public int num;
    public IPlaceableObj placeableObj;
    
    // Start is called before the first frame update
    public GridData(int num)
    {
        this.num = num;
    }

    public GridData(int num, IPlaceableObj placeableObj)
    {
        this.num = num;
        this.placeableObj = placeableObj;
    }

    public GridData()
    {
        this.num = 0;
    }

}
