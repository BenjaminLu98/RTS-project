using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keep : IBuilding
{
    private List<IUnit> trainableUnits;
    public List<IUnit> TrainableUnits { 
        get
        {
            return trainableUnits;
        }
        set
        {
            trainableUnits = value;
        }
    }

    private GridSystem gridSystem;
    public GridSystem GridSystem { 
        set
        {
            gridSystem = value;
        }
    }

    private bool isObtacle;
    public bool IsObstacle
    {
        get
        {
            return isObtacle;
        }
        set
        {
            isObtacle = value;
        }
    }

    public void placeAt(int x, int z)
    {
        if (gridSystem == null) Debug.Log(this.GetType().Name + ": gridSystem not loaded!");
        gridSystem.setValue(x, z, new GridData(100, this));
    }

    public void placeAt(Vector3 worldPosition)
    {
        if (gridSystem == null) Debug.Log(this.GetType().Name + ": gridSystem not loaded!");
        gridSystem.setValue(worldPosition, new GridData(100, this));
    }
}
