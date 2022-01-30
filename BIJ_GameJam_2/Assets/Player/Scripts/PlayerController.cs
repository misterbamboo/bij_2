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

    [SerializeField] private Transform hightPoint;

    private IPlayerInput playerInput;

    private Rigidbody rb;

    private Vector3 lastPos;
    private float targetHight;

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

        var rotation = Quaternion.LookRotation(direction);
        var wantedRotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);

        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, 0.3f); ;
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
        var origin = hightPoint.position;

        targetHight = height;

        var layerMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(origin, -Vector3.up, out RaycastHit rayHit, 100f, layerMask /* terrain layer*/))
        {
            targetHight = rayHit.point.y + height;
        }

        var destination = origin;
        destination.y = targetHight;
        Debug.DrawLine(origin, destination, Color.green);

        var targetPos = transform.position;
        targetPos.y = targetHight;

        transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
    }
}
