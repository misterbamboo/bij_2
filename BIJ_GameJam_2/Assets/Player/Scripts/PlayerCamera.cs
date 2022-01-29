using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform playerTransform;

    [SerializeField] private float range;
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;

    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
    }

    void Update()
    {
        transform.position = playerTransform.position + new Vector3(range + xOffset, yOffset, range + zOffset);
    }
}
