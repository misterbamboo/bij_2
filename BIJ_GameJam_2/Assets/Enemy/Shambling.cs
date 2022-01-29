using System.Collections;
using UnityEngine;

public class Shambling : MonoBehaviour
{
    public Vector3 Direction;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _directionTimerEnabled;

    // Start is called before the first frame update
    void Start()
    {
        //Direction = ChooseNextDirection();
        //transform.LookAt(transform.position + Direction);
        _speed = 50.0f;
        _directionTimerEnabled = true;

        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * _speed * Time.deltaTime;
        //Vector3.MoveTowards(transform.position, Destination, _speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        _directionTimerEnabled = false;
    }

    IEnumerator ChangeDirection()
    {
        while(_directionTimerEnabled)
        {
            Direction = ChooseNextDirection();
            transform.LookAt(transform.position + Direction);

            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }

    private Vector3 ChooseNextDirection()
    {
        var direction = Random.onUnitSphere;
        direction.y = 0;
        return direction;
    }
}
