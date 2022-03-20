using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUtil
{
    // Convert direction to Rotation.
    public static Quaternion getDirRotation(IUnit.dir dir)
    {
        switch (dir)
        {
            case IUnit.dir.forward:
                return Quaternion.LookRotation(Vector3.forward);
            case IUnit.dir.right:
                return Quaternion.LookRotation(Vector3.right);
            case IUnit.dir.backward:
                return Quaternion.LookRotation(Vector3.back);
            case IUnit.dir.left:
                return Quaternion.LookRotation(Vector3.left);
        }
        Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + ": no matching direction");
        return Quaternion.LookRotation(Vector3.forward);
    }

    // Get the normalized direction vector from the enum direction.
    public static Vector3 getDirVector(IUnit.dir dir)
    {
        switch (dir)
        {
            case IUnit.dir.forward:
                return Vector3.forward;
            case IUnit.dir.right:
                return Vector3.right;
            case IUnit.dir.backward:
                return Vector3.back;
            case IUnit.dir.left:
                return Vector3.left;
        }
        Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + ": no matching direction");
        return Vector3.forward;
    }

    public static Vector2Int getDirVector2D(IUnit.dir dir)
    {
        switch (dir)
        {
            case IUnit.dir.forward:
                return new Vector2Int(0, 1);
            case IUnit.dir.right:
                return new Vector2Int(1, 0);
            case IUnit.dir.backward:
                return new Vector2Int(0, -1);
            case IUnit.dir.left:
                return new Vector2Int(-1, 0);
        }
        Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + ": no matching direction");
        return new Vector2Int(0, 1);
    }

    // Convert a vector(from->to ) to the four direction.
    public static IUnit.dir getDir(Vector2Int from, Vector2Int to)
    {
        var fromPosition = GridSystem.current.getWorldPosition(from.x, from.y);
        var toPosition = GridSystem.current.getWorldPosition(to.x, to.y);

        float zDeg = Vector3.Angle(Vector3.forward, toPosition - fromPosition);
        float xDeg = Vector3.Angle(Vector3.right, toPosition - fromPosition);

        if (xDeg < 90f)
        {
            if (zDeg < 45f)
            {
                return IUnit.dir.forward;
            }
            else if (zDeg < 135f)
            {
                return IUnit.dir.right;
            }
            else
            {
                return IUnit.dir.backward;
            }
        }
        else
        {
            if (zDeg < 45f)
            {
                return IUnit.dir.forward;
            }
            else if (zDeg < 135f)
            {
                return IUnit.dir.left;
            }
            else
            {
                return IUnit.dir.backward;
            }
        }
    }



    public static IUnit.dir GetDirWithPF(List<GridData> path)
    {
        // If the unit already reached the targetPosition.
        if (path.Count == 1) return IUnit.dir.forward;

        var diff = path[path.Count - 2].Position - path[path.Count - 1].Position;
        //Debug.Log($"path[path.Count - 2]{path[path.Count - 2].Position},path[path.Count-1]:{path[path.Count - 1].Position}");
        if (diff == new Vector2Int(0, 1)) return IUnit.dir.forward;
        else if (diff == new Vector2Int(0, -1)) return IUnit.dir.backward;
        else if (diff == new Vector2Int(1, 0)) return IUnit.dir.right;
        else if (diff == new Vector2Int(-1, 0)) return IUnit.dir.left;
        else
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $"diff vector does not have an expected value. diff:{diff}");
            return IUnit.dir.forward;
        }

    }
}
