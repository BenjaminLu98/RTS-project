using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollapse : MonoBehaviour
{
    Vector3 originalPosition;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        targetPosition = transform.position - Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 0.5f);
    }
}
