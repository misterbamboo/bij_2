using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoMouvements : MonoBehaviour
{
    [SerializeField] private ArmAnimator leftArmAnimator;
    [SerializeField] private ArmAnimator rightArmAnimator;
    [SerializeField] private HairAnimator hairAnimator;

    [SerializeField] private float value;

    private float tick;
    private Vector3 lastPos;
    private bool isMoving;

    private void OnDrawGizmos()
    {
        UpdateValue();
    }

    void Update()
    {
        UpdateValue();
    }

    private void UpdateValue()
    {
        CheckMovement();
        Animate();
    }

    private void CheckMovement()
    {

        tick -= Time.deltaTime;
        if (tick < 0)
        {
            var distance = (lastPos - transform.position).magnitude;
            isMoving = Mathf.Abs(distance) > 0.001f;
            tick = +1;
            lastPos = transform.position;
        }
    }

    private void Animate()
    {
        value = isMoving ? value + Time.deltaTime : 0;

        var leftAngle = Mathf.Sin(value * 4);
        leftArmAnimator.SetAngleRate(leftAngle);
        hairAnimator.SetAngleRate(leftAngle);

        var rightAngle = Mathf.Sin((value + Mathf.PI) * 4);
        rightArmAnimator.SetAngleRate(rightAngle);
    }
}
