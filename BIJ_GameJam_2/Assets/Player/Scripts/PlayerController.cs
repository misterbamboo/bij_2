using Assets.SharedKernel.Inputs;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput PlayerInput { get; set; }

    private Rigidbody rb;

    [SerializeField] private float height = 2.0f;

    void Start()
    {
        PlayerInput = Assets.SharedKernel.Inputs.PlayerInput.Instance;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        var raw = new Vector3(PlayerInput.Horizontal, 0, PlayerInput.Vertical);
        var movement = Quaternion.AngleAxis(45, Vector3.up) * raw;

        var diff = height - transform.position.y;
        var yadjust = new Vector3(0, diff * Time.deltaTime * 100, 0);

        var force = movement + yadjust;
        rb.AddForce(force);
    }
}
