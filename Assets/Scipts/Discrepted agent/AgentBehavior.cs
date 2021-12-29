using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{

    public Agent agent;
    public Steering steering;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    { 
        agent = GetComponent<Agent>();

    }

    // Update is called once per frame
    public virtual void Update()
    {
        //update accelarate
        steering = getSteering();
        agent.accelarate = steering.accelerate;
        agent.angular = steering.angular;
    }

    public virtual Steering getSteering()
    {
        return new Steering();
    }

}
