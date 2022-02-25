using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pahtfinding
{
    private List<GridData> openList;
    private List<GridData> closeList;

    private List<GridData> FindPath(int startX, int startZ, int endX, int endZ)
    {
        GridData startGridData = GridSystem.current.getGridData(startX, startZ);
        openList = new List<GridData> { startGridData };
        closeList = new List<GridData>();
        
        for(int x=0; x < GridSystem.width; x++)
        {
            for(int z=0; z< GridSystem.height; z++)
            {
                var node = GridSystem.current.getGridData(x, z);
                node.GCost = int.MaxValue;
            }
        }
        return null;
    }
}

