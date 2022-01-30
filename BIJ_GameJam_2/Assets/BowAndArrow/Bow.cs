using Assets.SharedKernel.Inputs;
using System;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;

    [SerializeField] float arrowForce;

    [SerializeField] float arrowUpForce;

    [SerializeField] float fireRatePerSec;

    [SerializeField] PredictionManager predictionManager;

    private float shootAngle = 30.0f;

    private float lastFire;

    private IPlayerInput playerInput;

    [SerializeField] public float BoosterTime;
    private bool useThreeArrows;

    public bool UseSpeedyArrows;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        inputManager.OnPlayerAttack += HandleOnAttack;
    }
        
    void Start()
    {
        playerInput = PlayerInput.Instance;
    }

    private void HandleOnAttack(Vector3 target)
    {
        transform.LookAt(target);

        print($"HandleOnAttack - x : {target.x}; y : {target.y}, z : {target.z}");
        bool readyToFire = ReadyToFire();
        if (readyToFire && playerInput.Attack)
        {
            FireArrow(target);
        }

        if (BoosterTime > 0)
        {
            BoosterTime -= Time.deltaTime;
            var time = TimeSpan.FromSeconds(BoosterTime);

            useThreeArrows = time > TimeSpan.Zero;
        }
        
        // predictionManager.Predict(arrowPrefab, transform.position, GetArrowForce(transform.forward));
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
        // arrowRb.AddForce(GetArrowForce(arrow.transform.forward));      
        var velocity = CalcBallisticVelocityVector(transform.position, target, shootAngle);

        try
        {
            arrowRb.velocity = velocity;
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
    }

    private Vector3 GetArrowForce(Vector3 forwardVector)
    {
        var upForce = new Vector3(0, arrowUpForce, 0);
        var frontForce = forwardVector * arrowForce;
        return upForce + frontForce;
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
