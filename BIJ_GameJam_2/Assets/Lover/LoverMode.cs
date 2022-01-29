using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoverMode : MonoBehaviour
{
    private bool wasInLove;
    [SerializeField] private bool isInLove;

    [SerializeField] private float followHisLoveSpeed;

    [SerializeField] private Material loverMaterial;

    [SerializeField] private Material noneLoverMaterial;

    private List<LoverMode> closeLovers = new List<LoverMode>();

    private LoverMode targetLover;

    private Renderer loverRenderer;

    void Start()
    {
        loverRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        FindLover();
        FollowHisLove();
        UpdateLoverApparence();
    }

    private void FindLover()
    {
        if (isInLove && targetLover is null)
        {
            targetLover = FindNewNotInLoveLover();
        }
    }

    private void FollowHisLove()
    {

        if (isInLove && targetLover is not null)
        {
            if (StopFollowIfAlreadyInLove())
            {
                return;
            }
            FollowTargetLover();
        }
    }

    private bool StopFollowIfAlreadyInLove()
    {
        // Stop follow target if already become in love ... snif
        if (targetLover.isInLove)
        {
            targetLover = null;
            return true;
        }

        return false;
    }

    private void UpdateLoverApparence()
    {
        if (isInLove != wasInLove)
        {
            loverRenderer.material = isInLove ? loverMaterial : noneLoverMaterial;
        }
        wasInLove = isInLove;
    }

    private LoverMode FindNewNotInLoveLover()
    {
        float closestLoverDistance = float.MaxValue;
        LoverMode closestLover = null;
        foreach (var closeLover in closeLovers.Where(l => !l.isInLove))
        {
            var distance = (closeLover.transform.position - transform.position).magnitude;
            if (distance < closestLoverDistance)
            {
                closestLoverDistance = distance;
                closestLover = closeLover;
            }
        }
        return closestLover;
    }

    private void FollowTargetLover()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLover.transform.position, followHisLoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherLover = collision.gameObject.GetComponent<LoverMode>();
        if (otherLover is not null)
        {
            otherLover.isInLove = true;
            targetLover = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        KeepCloseLovers(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ReleaseCloseLovers(other);
    }

    private void KeepCloseLovers(Collider other)
    {
        var otherLover = other.GetComponent<LoverMode>();
        if (otherLover is not null)
        {
            closeLovers.Add(otherLover);
        }
    }

    private void ReleaseCloseLovers(Collider other)
    {
        var otherLover = other.GetComponent<LoverMode>();
        if (otherLover is not null && closeLovers.Contains(otherLover))
        {
            closeLovers.Remove(otherLover);
        }
    }
}
