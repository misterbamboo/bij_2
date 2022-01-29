using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAngle : MonoBehaviour
{
    private Vector3 lastPos;

    private bool followLookAt;

    void Start()
    {
        lastPos = transform.position;
        followLookAt = true;
    }

    void Update()
    {
        if (followLookAt)
        {
            var direction = transform.position - lastPos;
            transform.LookAt(transform.position + direction);
            lastPos = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        followLookAt = false;
    }
}
