using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    [SerializeField] Transform centerTarget;
    [SerializeField] Vector2 targetSize;

    [SerializeField] int numberOfPeople;

    [SerializeField] float sideBuffer;

    [SerializeField] GameObject prefab;

    void Start()
    {
        if (prefab == null)
        {
            throw new System.Exception("PeopleGenerator need a prefab to generate");
        }

        float sizeX = targetSize.x - sideBuffer;
        float sizeZ = targetSize.y - sideBuffer;

        GeneratePeople(GetCenterPos(), sizeX, sizeZ);
    }

    private void GeneratePeople(Vector3 center, float xSize, float zSize)
    {
        for (int i = 0; i < numberOfPeople; i++)
        {
            GenerateOnePerson(center, xSize, zSize);
        }
    }

    private void GenerateOnePerson(Vector3 center, float xSize, float zSize)
    {
        var randomPos = GenerateRandomPosition(center, xSize, zSize);
        Instantiate(prefab, randomPos, Quaternion.identity);
    }

    private static Vector3 GenerateRandomPosition(Vector3 center, float xSize, float zSize)
    {
        var randX = Random.Range(0, xSize);
        var randZ = Random.Range(0, zSize);

        var rawX = center.x + randX;
        var rawZ = center.z + randZ;

        var offsetX = -xSize / 2;
        var offsetZ = -zSize / 2;

        return new Vector3(rawX + offsetX, 0, rawZ + offsetZ);
    }

    private Vector3 GetCenterPos()
    {
        if (centerTarget != null)
        {
            return centerTarget.position;
        }

        return Vector3.zero;
    }
}
