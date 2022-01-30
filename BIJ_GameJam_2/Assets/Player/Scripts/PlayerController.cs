using Assets.SharedKernel.Inputs;
using UnityEngine;

public interface IPlayerController
{
    float Angle { get; set; }
    Transform transform { get; }
}

public class PlayerController : MonoBehaviour, IPlayerController
{
    public static IPlayerController Instance { get; private set; }
    public float Angle { get; set; }

    [SerializeField] private float height;

    [SerializeField] private float speed;

    private IPlayerInput playerInput;

    private Rigidbody rb;

    private Vector3 lastPos;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerInput = PlayerInput.Instance;
        rb = GetComponent<Rigidbody>();
        FixHeight();

        lastPos = transform.position + Vector3.forward;

        GameManager.Instance.MapBoundries.Register(transform);
    }

    void Update()
    {
        var direction = transform.position - lastPos;
        transform.LookAt(transform.position + direction);
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        rb.AddForce(Get45RotatedMovement());
        FixHeight();
    }

    private Vector3 Get45RotatedMovement()
    {
        var raw = GetRawInputMovement();
        return Quaternion.AngleAxis(Angle, Vector3.up) * raw;
    }

    private Vector3 GetRawInputMovement()
    {
        var timeSpeed = speed * Time.deltaTime;
        return new Vector3(playerInput.Horizontal * timeSpeed, 0, playerInput.Vertical * timeSpeed);
    }

    private void FixHeight()
    {
        var pos = rb.transform.position;
        pos.y = height;
        rb.transform.position = pos;
    }
}
