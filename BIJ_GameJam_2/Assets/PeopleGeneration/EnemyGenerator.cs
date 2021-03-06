using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] Transform centerTarget;

    [SerializeField] int numberOfEnemy;

    [SerializeField] float sideBuffer;

    [SerializeField] GameObject prefab;

    [SerializeField] float spawnMinHeight;

    [SerializeField] float spawnMaxHeight;

    void Start()
    {
        if (prefab == null)
        {
            throw new System.Exception("PeopleGenerator need a prefab to generate");
        }

        float sizeX = GameManager.Instance.MapSizeX - sideBuffer;
        float sizeZ = GameManager.Instance.MapSizeZ - sideBuffer;

        GeneratePeople(GetCenterPos(), sizeX, sizeZ);
    }

    private void GeneratePeople(Vector3 center, float xSize, float zSize)
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            bool putInLove = i == 0;
            GenerateOnePerson(center, xSize, zSize, putInLove);
        }
    }

    private void GenerateOnePerson(Vector3 center, float xSize, float zSize, bool putInLove)
    {
        var randomPos = GenerateRandomPosition(center, xSize, zSize);
        var instance = Instantiate(prefab, randomPos, Quaternion.identity);

        GameManager.Instance.MapBoundries.Register(instance.transform);
    }

    private Vector3 GenerateRandomPosition(Vector3 center, float xSize, float zSize)
    {
        var randX = Random.Range(0, xSize);
        var randZ = Random.Range(0, zSize);

        var rawX = center.x + randX;
        var rawZ = center.z + randZ;

        var offsetX = -xSize / 2;
        var offsetZ = -zSize / 2;
        
        var height = Random.Range(spawnMinHeight, spawnMaxHeight);
        return new Vector3(rawX + offsetX, height, rawZ + offsetZ);
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
