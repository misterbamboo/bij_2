using System;
using System.Collections;
using UnityEngine;

public class WalkAround : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField] private float destinationMinDistance;

    [SerializeField] private float destinationMaxDistance;

    [SerializeField] private float destinationRadius;

    [SerializeField] private float destinationMinCooldown;

    [SerializeField] private float destinationMaxCooldown;

    [SerializeField] private float abandonTargetTime;

    [SerializeField] private Lover lover;

    private Vector3 target;
    private float targetSince;
    private float currentCooldown;

    public static int _nextId;
    [SerializeField] int followId;
    [SerializeField] private int id;

    private void OnDrawGizmosSelected()
    {
        if (target != Vector3.zero)
        {
            Debug.DrawLine(target, target + Vector3.up, Color.red);
        }
    }

    void Start()
    {
        _nextId++;
        id = _nextId;
    }

    private void FixedUpdate()
    {
        if (!lover.HaveLoverInTarget)
        {
            MoveAimlessly();
        }
    }

    private void MoveAimlessly()
    {
        // In cooldown, wait
        if (IsInCooldown())
        {
            printId($"in cooldown {currentCooldown}");
            target = Vector3.zero;
            currentCooldown -= Time.deltaTime;
            return;
        }
        currentCooldown = 0;

        // Find a new target
        if (HasTarget())
        {
            printId("has target");
            targetSince += Time.deltaTime;
            MoveToTarget();
            LookToTarget();
        }
        else
        {
            printId("new Target");
            DefineNewTarget();
        }

        // Check if at destination
        if (InDestinationRadius())
        {
            printId("at destination");
            target = Vector3.zero;
            SetCooldown();
            return;
        }

        // Abandon after some time
        if (TimeToAbandonTarget())
        {
            printId("abandon target and define new");
            target = Vector3.zero;
            SetCooldown();
            return;
        }
    }

    private bool TimeToAbandonTarget()
    {
        return targetSince > abandonTargetTime;
    }

    private bool IsInCooldown()
    {
        return currentCooldown > 0f;
    }

    private bool InDestinationRadius()
    {
        var distance = MathF.Abs((transform.position - target).magnitude);
        return distance < destinationRadius;
    }

    private void SetCooldown()
    {
        currentCooldown = UnityEngine.Random.Range(destinationMinCooldown, destinationMaxCooldown);
    }

    private bool HasTarget()
    {
        return target != Vector3.zero;
    }

    private void DefineNewTarget()
    {
        var direction = ChooseNextDirection();
        target = transform.position + direction;
        targetSince = 0;

        float height = 0.15f;
        var origin = ChangeHeight(transform.position, height);
        var heightTarget = ChangeHeight(target, height);
        Debug.DrawLine(origin, heightTarget, Color.green);
        if (Physics.Raycast(origin, heightTarget, out RaycastHit rayHit, 1f, 7 /* floor layer*/))
        {
            target = rayHit.point;
        }
    }

    private Vector3 ChangeHeight(Vector3 vector, float height)
    {
        vector.y = height;
        return vector;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }

    private void LookToTarget()
    {
        if (target == Vector3.zero)
        {
            return;
        }

        // rotation 2x speed time (so 0.5s to rotate)
        var t = Math.Clamp(targetSince * 2f, 0, 1);

        var forward = target - transform.position;
        if (forward == Vector3.zero)
        {
            return;
        }

        var lookOn = Quaternion.LookRotation(forward);
        lookOn = LimitFlickeringAngle(lookOn);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOn, t);
    }

    private static Quaternion LimitFlickeringAngle(Quaternion targetRotation)
    {
        // avoid flickering (allow only rotation around Y axis)
        var eulerAngles = targetRotation.eulerAngles;
        eulerAngles.z = 0;
        eulerAngles.x = 0;
        targetRotation.eulerAngles = eulerAngles;
        return targetRotation;
    }

    private Vector3 ChooseNextDirection()
    {
        var x = Mathf.Lerp(destinationMinDistance, destinationMaxDistance, UnityEngine.Random.Range(0, 1f));
        bool xSign = UnityEngine.Random.Range(0, 2) > 0;
        x *= xSign ? 1 : -1;

        var z = Mathf.Lerp(destinationMinDistance, destinationMaxDistance, UnityEngine.Random.Range(0, 1f));
        bool zSign = UnityEngine.Random.Range(0, 2) > 0;
        z *= zSign ? 1 : -1;

        return new Vector3(x, 0, z);
    }

    private void printId(object value)
    {
        if (id == followId)
        {
            print(value);
        }
    }
}
