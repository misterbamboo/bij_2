using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shambling : MonoBehaviour
{
    public Vector3 Direction;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _directionTimerEnabled;

    [SerializeField]
    private float _giveSadnessSpeed;

    private List<LoveMeter> _loversInProximity = new();

    // Start is called before the first frame update
    void Start()
    {
        _directionTimerEnabled = true;

        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        MoveAimlessly();
        SpreadSadness();
    }

    void OnTriggerEnter(Collider other)
    {
        var meter = other.GetComponent<LoveMeter>();
        if (meter != null && !_loversInProximity.Contains(meter))
        {
            _loversInProximity.Add(meter);
        }
    }

    void OnTriggerExit(Collider other)
        => _loversInProximity.Remove(other.GetComponent<LoveMeter>());

    private void OnDestroy() => _directionTimerEnabled = false;

    private void MoveAimlessly()
        => transform.position += Direction * _speed * Time.deltaTime;

    private void SpreadSadness()
    {
        foreach (var meter in _loversInProximity)
        {
            if (meter != null)
                meter.ModifyLove(-_giveSadnessSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChangeDirection()
    {
        while (_directionTimerEnabled)
        {
            Direction = ChooseNextDirection();
            transform.LookAt(transform.position - Direction);

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
