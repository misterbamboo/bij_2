using Assets.SharedKernel.Inputs;
using System;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;

    [SerializeField] float fireRatePerSec;

    [SerializeField] PredictionManager predictionManager;

    private float shootAngle = 30.0f;

    private float lastFire;

    private IPlayerInput playerInput;

    public bool UseThreeArrows;

    public bool UseSpeedyArrows;

    private InputManager inputManager;

    private void Awake()
    {
        playerInput = PlayerInput.Instance;
        inputManager = FindObjectOfType<InputManager>();
        if (inputManager != null)
        {
            inputManager.OnPlayerAttack += HandleOnAttack;
        }
    }

    private void HandleOnAttack(Vector3 target)
    {
        transform.LookAt(target);

        // print($"HandleOnAttack - x : {target.x}; y : {target.y}, z : {target.z}");
        bool readyToFire = ReadyToFire();
        if (readyToFire && playerInput.Attack)
        {
            FireArrow(target);
        }

        predictionManager.Predict(arrowPrefab, transform.position, CalcBallisticVelocityVector(transform.position, target, shootAngle));
    }

    private bool ReadyToFire()
    {
        var fireRate = UseSpeedyArrows ? fireRatePerSec * 3 : fireRatePerSec;

        lastFire += Time.deltaTime;
        bool readyToFire = lastFire > 1 / fireRate;
        return readyToFire;
    }

    private void FireArrow(Vector3 target)
    {
        GameManager.Instance.GameEvent(Assets.GameProgression.GameEvents.ArrowFired);

        lastFire = 0;
        var arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        ApplyForce(arrow, target);

        if (UseThreeArrows)
        {
            var leftArrow = Instantiate(arrowPrefab, transform.position + Vector3.left, transform.rotation);
            ApplyForce(leftArrow, target);
            var rightArrow = Instantiate(arrowPrefab, transform.position + Vector3.back, transform.rotation);
            ApplyForce(rightArrow, target);
        }
    }

    private void ApplyForce(GameObject arrow, Vector3 target)
    {
        var arrowRb = arrow.GetComponent<Rigidbody>();
        var velocity = CalcBallisticVelocityVector(transform.position, target, shootAngle);
        arrowRb.velocity = velocity;
    }

    Vector3 CalcBallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        velocity = float.IsNaN(velocity) ? 0.0f : velocity;
        return velocity * direction.normalized;
    }
}
