using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    protected float hp;
    protected float rotateSpeed=180f;
    protected bool isMoving=false;
    protected bool isRotating=false;
    protected int x;
    protected int z;
    protected int width;
    protected int height;
    protected Vector3 targetPosition;
    protected float trueSpeed=2.0f;
    protected Quaternion targetRotation;
    protected Transform modelTransform;
    protected StateManager state;
    protected TargetSelector ts=new TargetSelector();
    private float CountDown;
    private Vector3 moveDirection;
    private float maxSpeed = 3f;


    protected void Start()
    {
        animator.SetBool("isRunning", false); animator.SetBool("isAttacking", false); animator.SetBool("isRotating", false);
        targetPosition = transform.position;

        modelTransform = transform.GetChild(0);
        targetRotation = modelTransform.rotation;

        InitializeStateManager();
    }
    //Note that you need to convert float hp to integer.
    public int HP => throw new System.NotImplementedException();

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

    // TODO: this function should change the target position, not directly change the position. the state should handle the movement.
    //TODO: minimum rotation.
    public void moveTo(Vector3 WorldPosition, float speed)
    {
        int x, z;
        GridSystem.current.getXZ(WorldPosition, out x, out z);
        targetPosition = GridSystem.current.getWorldPosition(x, z);
        moveDirection = (targetPosition - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(moveDirection);
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
    public float AttackInterval { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float AttackRange { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }

    public bool placeAt(int x, int z)
    {

        bool isSuccess = GridSystem.current.setValue(x, z, 100, this);
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
        bool isSuccess = GridSystem.current.setValue(worldPosition, 100, this, width, height);
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

    // StateMachine configuration.
    private void InitializeStateManager()
    {
        state = new StateManager();
        state.addStatus("Idle", new State()
            .OnStart(() => { CountDown = 4.0f; animator.SetBool("isRunning", false); animator.SetBool("isAttacking", false); animator.SetBool("isRotating", false); Debug.Log("Idle onStart"); })
            .OnStay(() => { if (CountDown > 0) CountDown -= Time.deltaTime; })
            .OnExit(() => Debug.Log("exit idle"))
            .addCondition("Rotate", () => !targetRotation.Equals(modelTransform.rotation))
            .addCondition("Run", () => !targetPosition.Equals(transform.position))
            .addCondition("Attack", () => ts.getTarget() != null && CountDown <= 0)
            );

        state.addStatus("Rotate", new State()
            .addCondition("Idle", () => targetPosition.Equals(transform.position) && targetRotation.Equals(modelTransform.rotation))
            .addCondition("Run", () => targetRotation.Equals(modelTransform.rotation) && !targetPosition.Equals(transform.position))
            .OnStart(() => { animator.SetBool("isRunning", false); animator.SetBool("isAttacking", false); animator.SetBool("isRotating", true); Debug.Log("rotate onStart"); })
            .OnStay(() =>
            {
                Quaternion tempRotation = Quaternion.RotateTowards(modelTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                modelTransform.rotation = tempRotation;
            })
            .OnExit(() => { Debug.Log("Rotate exit"); })
            );
        state.addStatus("Run", new State()
            .addCondition("Idle", () => targetPosition.Equals(transform.position))
            .addCondition("Attack", () => ts.getTarget() != null && CountDown <= 0)
            .addCondition("Rotate", () => !targetRotation.Equals(modelTransform.rotation))
            .OnStart(() => { CountDown = 2.0f; animator.SetBool("isRunning", true); animator.SetBool("isAttacking", false); animator.SetBool("isRotating", false); ; Debug.Log("run onStart"); })
            .OnStay(() =>
            {
                if (CountDown > 0) CountDown -= Time.deltaTime;
                if (!targetPosition.Equals(transform.position) && GridSystem.current.checkOccupationExcept(x, z, this))
                {
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
                    GridSystem.current.setValue(transform.position, 10, this, width, height);
                }
            })
        );

        state.addStatus("Attack", new State()
            // TODO: add animation stop
            .addCondition("Idle", () => targetPosition.Equals(transform.position))
            .addCondition("Run", () => !targetPosition.Equals(transform.position))
            .addCondition("Rotate", () => !targetRotation.Equals(modelTransform.rotation))
            .OnStart(() => { animator.SetBool("isRunning", false); animator.SetBool("isAttacking", true); animator.SetBool("isRotating", false); })
            .OnStay(() =>
            {

            })
            .OnExit(() => animator.SetBool("isAttacking", false))
            );
        state.Name = "Idle";
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
        state.onUpdate();
    }
}
