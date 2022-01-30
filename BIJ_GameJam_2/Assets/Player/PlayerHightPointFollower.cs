using UnityEngine;

public class PlayerHightPointFollower : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update()
    {
        var pos = player.position;
        pos.y -= 1;
        transform.position = pos;
    }
}
