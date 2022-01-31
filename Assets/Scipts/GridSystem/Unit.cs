using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    protected float hp;
    //Note that you need to convert float hp to integer.
    public int HP => throw new System.NotImplementedException();

    protected float rotateSpeed=180f;
    protected bool isMoving=false;
    protected bool isRotating=false;
    protected int x;
    protected int z;
    protected Vector3 targetPosition;
    protected float trueSpeed;
    protected Quaternion targetRotation;
    protected Transform modelTransform;

    private void Start()
    {
        modelTransform = transform.GetChild(0);
    }
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

    //TODO: minimum rotation.
    public void moveTo(Vector3 WorldPosition, float speed)
    {
        int x, z;
        GridSystem.current.getXZ(WorldPosition, out x, out z);
        targetPosition = GridSystem.current.getWorldPosition(x, z);
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (speed > maxSpeed)
        {
            trueSpeed = maxSpeed;
        }
        else
        {
            trueSpeed = speed;
        }

        //Rotate.
        targetRotation = Quaternion.LookRotation(moveDirection);

        //If rotation is not complete, rotate first. Otherwise pass the rotation phase and do the Movement.
        //Note that we rotate the model transform but translate the parent transform.
        if (!targetRotation.Equals(modelTransform.rotation))
        {
            isRotating = true;
            Quaternion tempRotation = Quaternion.RotateTowards(modelTransform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
            modelTransform.rotation = tempRotation;
        }
        else
        {
            isRotating = false;
            //Move.
            if (!targetPosition.Equals(transform.position) && GridSystem.current.checkOccupationExcept(x, z, this))
            {
                isMoving = true;
                GridSystem.current.removeValue(transform.position, width, height);

                //Make sure the unit will finally arrive exactly at the target position.
                if ((targetPosition - transform.position).magnitude > Time.deltaTime * trueSpeed)
                {
                    transform.position += Time.deltaTime * trueSpeed * moveDirection;
                }
                else
                {
                    transform.position = targetPosition;
                    isMoving = false;
                }
                GridSystem.current.setValue(transform.position, new GridData(10, this), width, height);
                
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

    public float attack(int x, int z, float expectedDamage)
    {
        if(GridSystem.current.checkOccupation(x, z))
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod()+":No unit in the target position(${x},${z})");
            return 0;
        }
        //Find the target unit

        //deal damage

        //return actual damage
        return 0;
    }

    public bool recieveDamage(float expectedDamage, IUnit.DamageType type)
    {
        //calculate actual damage

        //apply damage

        //if the damage is greater than hp, then the unit will die.


        return true;
    }


    private void Update()
    {
        if (isRotating||isMoving)
        {
            moveTo(targetPosition, trueSpeed);
        }

        
    }

}
