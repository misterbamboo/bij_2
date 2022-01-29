using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HateEachOther : MonoBehaviour
{
    private List<Collider> _collidingWith = new();
    private Collider _closest = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_closest != null)
        {
            var shambling = _closest.GetComponent<Shambling>();
            shambling.Direction = -shambling.Direction;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_collidingWith.Contains(other))
            _collidingWith.Add(other);

        _closest = GetClosest();
    }

    void OnTriggerExit(Collider other)
    {
        _collidingWith.Remove(other);
        _closest = GetClosest();
    }

    private Collider GetClosest() =>
        _collidingWith
        .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
        .FirstOrDefault();

}
