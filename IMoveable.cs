using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


interface IMoveable
{
    /// <summary>
    /// Rotate to the target and move to the target position with animation. 
    /// </summary>
    /// <param name="worldPosition">target position</param>
    /// <param name="speed"></param>
    public void moveTo(Vector3 worldPosition, float speed);
    //Move to the target position with animation
    public void moveTo(int x, int z, float speed);
}

