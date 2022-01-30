using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private IPlayerController playerController;
    private Transform playerTransform;

    [SerializeField] private float range;
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;

    [SerializeField] private float angleYOffset;
    [SerializeField] private float angleXOffset;

    [SerializeField, Range(0, 1)]
    private float positionLerp;

    [SerializeField, Range(0, 1)]
    private float angleLerp;

    private bool mouseDown;
    private float currentDrag;
    private float currentDiff;

    private float currentAngle;
    private float initialDragAngle;
    private Vector3 targetPos;
    private float targetAngle;

    void Start()
    {
        playerController = PlayerController.Instance;
        playerTransform = PlayerController.Instance.transform;
    }

    void Update()
    {
        TrackMouseClick();
        FollowDrag();
        PlaceCameraAroundPlayer();
        ChangeCameraAngle();

        playerController.Angle = currentAngle + angleYOffset;
    }

    private void TrackMouseClick()
    {
        mouseDown = Input.GetMouseButton(1);
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
        {
            MouseStateChange();
        }
    }

    private void MouseStateChange()
    {
        if (mouseDown)
        {
            print("clicked");
            currentDrag = Input.mousePosition.x;
            initialDragAngle = currentAngle;
        }
        else
        {
            print("unclicked");
        }
    }

    private void FollowDrag()
    {
        if (mouseDown)
        {
            currentDiff = currentDrag - Input.mousePosition.x;
            print(currentDiff);
        }
    }

    private void PlaceCameraAroundPlayer()
    {
        var basePos = new Vector3(range + xOffset, yOffset, range + zOffset);
        var newOffset = Quaternion.Euler(0, currentAngle, 0) * basePos;
        targetPos = playerTransform.position + newOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, positionLerp);
    }

    private void ChangeCameraAngle()
    {
        targetAngle = initialDragAngle + currentDiff;

        var targetRotation = Quaternion.Euler(angleXOffset, targetAngle + angleYOffset, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, angleLerp);

        currentAngle = targetAngle;
    }
}
