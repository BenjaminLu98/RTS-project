using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float horizontalSpeed=10f;
    float verticalSpeed=10f;
    float scollSpeed = 500f;
    float maxHeight = 40f;
    float minHeight = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

       

        float hmove = Input.GetAxis("Horizontal");

        float vmove = Input.GetAxis("Vertical");

        float umove = -Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hmove *= 5.0f;
            vmove *= 5.0f;
            umove *= 5.0f;
        }
        if (transform.position.y + umove * scollSpeed * Time.deltaTime <= minHeight)
        {
            umove=0.0f;
        }
        else if(transform.position.y + umove * scollSpeed * Time.deltaTime >= maxHeight)
        {
            umove = 0.0f;
        }

        Vector3 totalMove=new Vector3(hmove*horizontalSpeed*Time.deltaTime,umove*scollSpeed*Time.deltaTime,vmove * verticalSpeed * Time.deltaTime);
        transform.position += totalMove;
        
        


    }
}
