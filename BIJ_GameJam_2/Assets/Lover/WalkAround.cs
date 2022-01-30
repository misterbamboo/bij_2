using System.Collections;
using UnityEngine;

public class WalkAround : MonoBehaviour
{
    public Vector3 Direction;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _directionTimerEnabled;

    [SerializeField] private Lover lover;

    void Start()
    {
        _directionTimerEnabled = true;

        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        if (!lover.IsInLove)
        {
            MoveAimlessly();
        }
    }

    private void OnDestroy() => _directionTimerEnabled = false;

    private void MoveAimlessly()
        => transform.position += Direction * _speed * Time.deltaTime;

    IEnumerator ChangeDirection()
    {
        while (_directionTimerEnabled)
        {
            if (!lover.IsInLove)
            {
                Direction = ChooseNextDirection();
                transform.LookAt(transform.position - Direction);
            }

            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
        }
    }

    private Vector3 ChooseNextDirection()
    {
        var direction = Random.onUnitSphere;
        direction.y = 0;
        return direction;
    }
}
