using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LoveMeter))]
public class Lover : MonoBehaviour
{
    private bool wasInLove;
    public bool IsInLove => isInLove;

    [SerializeField] private bool isInLove;

    [SerializeField] private Transform charactertransform;

    [SerializeField] private float followHisLoveSpeed;

    [SerializeField] private float giveLoveSpeed;

    [SerializeField] private Material loverMaterial;

    [SerializeField] private Material noneLoverMaterial;

    [SerializeField] private Renderer[] loverRenderers;

    private List<Lover> closeLovers = new List<Lover>();

    private Lover targetLover;
    public bool HaveLoverInTarget => targetLover != null;

    private float lastDirection;

    void Start()
    {
        lastDirection = Random.Range(0, MathF.PI * 5);
    }

    void Update()
    {
        if (isInLove)
        {
            FindLover();
            FollowHisLove();
            GiveLoveToTarget();
        }

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

    public void PutInLove()
    {
        if (IsInLove) return;

        isInLove = true;
        GameManager.Instance.GameCounterChanged();
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
            foreach (var loverRenderer in loverRenderers)
            {
                loverRenderer.material = isInLove ? loverMaterial : noneLoverMaterial;
            }
        }
        wasInLove = isInLove;
    }

    private Lover FindNewNotInLoveLover()
    {
        float closestLoverDistance = float.MaxValue;
        Lover closestLover = null;
        foreach (var closeLover in closeLovers.Where(l => !l.isInLove))
        {
            var distance = (closeLover.transform.position - charactertransform.position).magnitude;
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
        charactertransform.position = Vector3.MoveTowards(charactertransform.position, targetLover.transform.position, followHisLoveSpeed * Time.deltaTime);
    }

    private void GiveLoveToTarget()
    {
        if (targetLover is not null)
        {
            var loveMeter = targetLover.GetComponent<LoveMeter>();
            loveMeter.ModifyLove(giveLoveSpeed * Time.deltaTime);
            if (loveMeter.IsFull)
            {
                targetLover.isInLove = true;
                GameManager.Instance.GameCounterChanged();
            }
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
        var otherLover = other.GetComponent<Lover>();
        if (otherLover is not null)
        {
            closeLovers.Add(otherLover);
        }
    }

    private void ReleaseCloseLovers(Collider other)
    {
        var otherLover = other.GetComponent<Lover>();
        if (otherLover is not null && closeLovers.Contains(otherLover))
        {
            closeLovers.Remove(otherLover);
        }
    }
}
