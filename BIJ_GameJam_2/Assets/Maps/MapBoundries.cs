using System.Collections.Generic;
using UnityEngine;

public class MapBoundries : MonoBehaviour
{
    private List<Transform> transforms = new();

    private Rect boudries;

    public void Register(Transform transform)
    {
        transforms.Add(transform);
    }

    private void Start()
    {
        var mapSizeX = GameManager.Instance.MapSizeX;
        var mapSizeZ = GameManager.Instance.MapSizeZ;

        var xMin = -mapSizeX / 2;
        var zMin = -mapSizeZ / 2;

        boudries = new Rect(xMin, zMin, mapSizeX, mapSizeZ);
    }

    void Update()
    {
        foreach (var goTransform in transforms)
        {
            var pos3 = goTransform.position;
            var pos = new Vector2(pos3.x, pos3.z);

            if (!boudries.Contains(pos))
            {
                ContraintToBounds(goTransform);
            }

            HardFloorLimit(goTransform);
        }
    }

    private void ContraintToBounds(Transform goTransform)
    {
        var pos = goTransform.position;
        if (pos.x < boudries.xMin)
        {
            pos.x = boudries.xMin;
        }
        else if (pos.x > boudries.xMax)
        {
            pos.x = boudries.xMax;
        }

        if (pos.z < boudries.yMin)
        {
            pos.z = boudries.yMin;
        }
        else if (pos.z > boudries.yMax)
        {
            pos.z = boudries.yMax;
        }
        goTransform.position = pos;
    }

    private void HardFloorLimit(Transform goTransform)
    {
        if (goTransform.position.y < 0)
        {
            var pos = goTransform.position;
            pos.y = 0;
            goTransform.position = pos;
        }
    }
}
