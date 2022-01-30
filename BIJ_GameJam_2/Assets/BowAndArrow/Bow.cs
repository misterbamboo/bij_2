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

    private float lastFire;

    private IPlayerInput playerInput;

    void Start()
    {
        playerInput = PlayerInput.Instance;
    }

    void Update()
    {
        bool readyToFire = ReadyToFire();
        if (readyToFire && playerInput.Attack)
        {
            FireArrow();
        }

        predictionManager.Predict(arrowPrefab, transform.position, GetArrowForce(transform.forward));
    }

    private bool ReadyToFire()
    {
        lastFire += Time.deltaTime;
        bool readyToFire = lastFire > 1 / fireRatePerSec;
        return readyToFire;
    }

    private void FireArrow()
    {
        lastFire = 0;
        var arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        ApplyForce(arrow);
    }

    private void ApplyForce(GameObject arrow)
    {
        var arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.AddForce(GetArrowForce(arrow.transform.forward));
    }

    private Vector3 GetArrowForce(Vector3 forwardVector)
    {
        var upForce = new Vector3(0, arrowUpForce, 0);
        var frontForce = forwardVector * arrowForce;
        return upForce + frontForce;
    }
}
