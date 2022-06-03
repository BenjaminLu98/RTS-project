using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridSystemTest
{

    // A Test behaves as an ordinary method
    [Test]
    public void getGridDataShouldacceptReasonableParameter()
    {
        var node = GridSystem.current.getGridData(99, 99);
        Assert.IsNotNull(node);
        
    }

    [Test]
    public void getGridDataShouldRejectNegative()
    {
        var node = GridSystem.current.getGridData(-1, -1);
        Assert.IsNull(node);
        LogAssert.Expect(LogType.Error, "GridData getGridData(Int32, Int32): (-1,-1) is not a valid position in the grid system");
    }
    [Test]
    public void getGridDataShouldRejectTooLarge()
    {
        var node = GridSystem.current.getGridData(100,100);
        Assert.IsNull(node);
        LogAssert.Expect(LogType.Error, "GridData getGridData(Int32, Int32): (100,100) is not a valid position in the grid system");
    }

    [Test]
    public void getWorldPositionShouldReturnCorrectPosition()
    {
        var position = GridSystem.current.getWorldPosition(10, 11);
        var expectedPosition = new Vector3(10 * GridSystem.current.sideLength, 1 * GridSystem.current.sideLength, 11 * GridSystem.current.sideLength);
        Assert.AreEqual(position, expectedPosition);
    }

    [Test]
    public void getWorldPositionShouldRejectTooLargePosition()
    {
        LogAssert.ignoreFailingMessages = true;
        var position = GridSystem.current.getWorldPosition(GridSystem.current.width+1, GridSystem.current.height+1);
        Assert.AreEqual(position,new Vector3(0,0,0));
    }

    [Test]
    public void getWorldPositionShouldRejectNegativePosition()
    {
        LogAssert.ignoreFailingMessages = true;
        var position = GridSystem.current.getWorldPosition(-1, -1);
        Assert.AreEqual(position, new Vector3(0, 0, 0)); 
    }

    [Test]
    public void setHeightGetHeight()
    {
        GridSystem.current.setHeight(15, 20, 3);
        Assert.AreEqual(GridSystem.current.getHeight(15, 20), 3);
    }
    [Test]
    public void setHeightShouldRejectNegativeHeight()
    {
        Assert.IsFalse(GridSystem.current.setHeight(15, 20, -1));
    }

    [Test]
    public void setValueGetValue()
    {
        LogAssert.ignoreFailingMessages = true;
        GridSystem.current.setValue(5, 5, 10, null);
        var node = GridSystem.current.getGridData(5, 5);
        Assert.AreEqual(node.Num, 10);
    }

    [Test]
    public void checkOccupationShouldFindOccupation()
    {
        LogAssert.ignoreFailingMessages = true;
        GridSystem.current.setValue(5, 6, 10, null);
        var result = GridSystem.current.checkOccupation(5, 6);
        Assert.IsFalse(result);
        GridSystem.current.removeValue(5, 6);
        var result2 = GridSystem.current.checkOccupation(5, 6);
        Assert.IsTrue(result2);
    }

    [Test]
    public void checkOccupationShouldFindOccupationWhenWidthHeightIsNot1()
    {
        LogAssert.ignoreFailingMessages = true;
        GridSystem.current.setValue(5, 7, 10, null);
        GridSystem.current.setValue(5, 8, 10, null);
        GridSystem.current.setValue(6, 6, 10, null);
        GridSystem.current.setValue(7, 6, 10, null);
        var result = GridSystem.current.checkOccupation(5, 6, 2, 3);
        Assert.IsFalse(result);
        GridSystem.current.removeValue(5, 6);
        var result2 = GridSystem.current.checkOccupation(5, 6, 1, 2);
        Assert.IsFalse(result2);
        var result3 = GridSystem.current.checkOccupation(5, 6, 1, 1);
        Assert.IsTrue(result3);
    }

    [Test]
    public void PathfindTest()
    {
        PathFinding pf = new PathFinding();
        var path = pf.FindPath(7, 4, 6, 6,true);
        foreach(var node in path)
        {
            Debug.Log(node.Position);
        }
          
    }

    [Test]
    public void PathfindTestWithOccupation()
    {
        PathFinding pf = new PathFinding();
        var path1 = pf.FindPath(1, 1, 1, 4,true);
        GridSystem.current.setValue(1, 2, 0 , null);
        var path2 = pf.FindPath(1, 1, 1, 4,true);
        Assert.AreNotEqual(path1, path2);
        foreach (var node in path2)
        {
            Debug.Log(node.Position);
        }
    }

    [Test]
    public void UtilCalculateDistanceShouldReturnCorrectDistance()
    {
        Assert.IsTrue(float.Equals(GridUtils.calculateDistance(3, 3, 5, 5),2f*Mathf.Pow(2f,0.5f)*GridSystem.current.sideLength));
        
    }


}
