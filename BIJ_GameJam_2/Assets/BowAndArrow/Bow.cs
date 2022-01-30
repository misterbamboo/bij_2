using Assets.SharedKernel.Inputs;
using System;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;

    [SerializeField] float arrowForce;

    [SerializeField] float arrowUpForce;

    [SerializeField] float fireRatePerSec;

    private float lastFire;

    private IPlayerInput playerInput;
    
    public bool UseThreeArrows;
    public bool UseSpeedyArrows;
    
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
    }

    private bool ReadyToFire()
    {
        var fireRate = UseSpeedyArrows ? fireRatePerSec * 3 : fireRatePerSec;

        lastFire += Time.deltaTime;
        bool readyToFire = lastFire > 1 / fireRate;
        return readyToFire;
    }

    private void FireArrow()
    {
        lastFire = 0;
        var arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        ApplyForce(arrow);

        if (UseThreeArrows)
        {
            var leftArrow = Instantiate(arrowPrefab, transform.position + Vector3.left, transform.rotation);
            ApplyForce(leftArrow);
            var rightArrow = Instantiate(arrowPrefab, transform.position + Vector3.back, transform.rotation);
            ApplyForce(rightArrow);
        }
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
