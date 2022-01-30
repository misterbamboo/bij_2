using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimator : MonoBehaviour
{
    [SerializeField] float angle;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAngleRate(float rate)
    {
        var rotation = transform.rotation;
        var currentAngle = rotation.eulerAngles;
        currentAngle.x = rate * angle;
        rotation.eulerAngles = currentAngle;
        transform.rotation = rotation;
    }
}
