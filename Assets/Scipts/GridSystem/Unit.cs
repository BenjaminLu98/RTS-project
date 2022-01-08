using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    public int HP => throw new System.NotImplementedException();

    protected float rotateSpeed;
    protected bool isMoving;
    protected int x;
    protected int z;
    protected Vector3 targetPosition;
    protected float trueSpeed;
    protected Quaternion targetRotation;
    public Vector2Int Position
    {
        get
        {
            return new Vector2Int(x, z);
        }
    }

    protected bool isObstacle;
    public bool IsObstacle {
        get
        {
            return isObstacle;
        }
        set
        {
            isObstacle = value;
        }
    }

    protected int width;
    protected int height;
    public Vector2Int Size {
        set
        {
            width = value.x;
            height = value.y;
        }
        get
        {
            return new Vector2Int(width, height);
        }
    }

    public void moveTo(Vector3 WorldPosition, float speed)
    {
        targetPosition = WorldPosition;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        //Rotate.
        targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = targetRotation;

        //Move.
        if (!targetPosition.Equals(transform.position))
        {
            isMoving = true;
            if (speed > maxSpeed)
            {
                trueSpeed = maxSpeed;
            }
            else
            {
                trueSpeed = speed;
            }

            //Make sure the unit will finally arrive exactly at the target position.
            if((targetPosition - transform.position).magnitude > Time.deltaTime * trueSpeed)
            {
                transform.position += Time.deltaTime * trueSpeed * moveDirection;
            }
            else
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void MoveTo(int x, int z, float speed)
    {
        Vector3 targetPosition = GridSystem.current.getWorldPosition(x, z);
        moveTo(targetPosition, speed);
    }

    protected Animator animator;

    public Animator Animator
    {
        get
        {
            return animator;
        }
    }

    public float PhysicalAttack { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float MagicAttack { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float PhysicalDefence { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float MagicDefence { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Mana { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float AttackSpeed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float AttackInterval { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float AttackRange { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    float maxSpeed = 3f;
    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }

    public bool placeAt(int x, int z)
    {

        bool isSuccess = GridSystem.current.setValue(x, z, new GridData(100, this));
        if (isSuccess)
        {
            Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
            transform.position = truePosition;

            GridSystem.current.removeValue(this.x, this.z, width, height);

            this.x = x;
            this.z = z;
            return true;
        }
        return false;

    }

    public bool placeAt(Vector3 worldPosition)
    {
       
        bool isSuccess = GridSystem.current.setValue(worldPosition, new GridData(100, this), width, height);
        if (isSuccess)
        {
            int x, z;
            GridSystem.current.getXZ(worldPosition, out x, out z);
            Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
            transform.position = truePosition;

            GridSystem.current.removeValue(this.x, this.z, width, height);
            this.x = x;
            this.z = z;
            return true;
        }
        return false;

    }

    private void Update()
    {
        if (isMoving)
        {
            moveTo(targetPosition, trueSpeed);
        }
        
    }

}
