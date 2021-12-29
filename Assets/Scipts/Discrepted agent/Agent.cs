using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Vector3 speed;
    public float maxSpeed;
    public Vector3 rotationSpeed;
    public Vector3 accelarate;
    public float maxAccelarate;
    public Vector3 angular;
    void Start()
    {
        maxSpeed = 5.0f;
        maxAccelarate =2.0f;
        speed = new Vector3();
        rotationSpeed = new Vector3();
        accelarate = new Vector3();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //update position and rotation
        transform.Translate(speed*Time.deltaTime);
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public virtual void LateUpdate()
    {
        //update speed and rotationspeed
        speed += accelarate * Time.deltaTime;
        if (speed.magnitude > maxSpeed)
        {
            speed = speed.normalized * maxSpeed;
        }
        rotationSpeed += angular * Time.deltaTime;
        
    }
}
