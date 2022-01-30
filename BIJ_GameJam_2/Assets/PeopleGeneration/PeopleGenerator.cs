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
            bool putInLove = i == 0;
            GenerateOnePerson(center, xSize, zSize, putInLove);
        }
    }

    private void GenerateOnePerson(Vector3 center, float xSize, float zSize, bool putInLove)
    {
        var randomPos = GenerateRandomPosition(center, xSize, zSize);
        var instance = Instantiate(prefab, randomPos, Quaternion.identity);

        if (putInLove)
        {
            instance.GetComponentInChildren<Lover>().PutInLove();
        }
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
