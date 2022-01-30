using Assets.SharedKernel.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    IPlayerInput playerInput;

    public event Action<Vector3> OnPlayerAttack = delegate { };

    private void Start()
    {
        playerInput = PlayerInput.Instance;
    } 

    private void Update()
    {
        if (playerInput.Attack)
        {
            var pos = GetMouseRaycastPos();
            OnPlayerAttack(pos);
        }
    }

    Vector3 GetMouseRaycastPos()
    {
        int layer_mask = LayerMask.GetMask("Floor");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int range = 1000;

        if (Physics.Raycast(ray, out hit, range, layer_mask))
        {
            Debug.DrawRay(hit.point, Vector3.forward * 1000, Color.green, 2, false);
            return hit.point;
        }

        return Vector3.zero;
    }
}
