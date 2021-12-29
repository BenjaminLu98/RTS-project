using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : AgentBehavior
{
    public override Steering getSteering()
    {
        steering = new Steering();
        Vector3 diff = target - transform.position;
        float accelerate = diff.magnitude;
        if (accelerate > agent.maxAccelarate)
        {
            accelerate = agent.maxAccelarate;
        }
        steering.accelerate = (target - transform.position).normalized*accelerate;
        
        return steering;
    }

    public override void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var p = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(p);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit, 50000))
            {
                target=hit.point;
            }
        }
        //update accelarate
        steering = getSteering();
        agent.accelarate = steering.accelerate;
        agent.angular = steering.angular;
    }
}
