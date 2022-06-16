using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : Resource
{
    // Start is called before the first frame update
    void Awake()
    {
        positionInfo = new PositionInfo();
        positionInfo.width = 3;
        positionInfo.height = 3;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
