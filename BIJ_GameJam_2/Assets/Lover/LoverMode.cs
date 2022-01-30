using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LoveMeter))]
public class LoverMode : MonoBehaviour
{
    private bool wasInLove;

    [SerializeField] private Transform charactertransform;

    [SerializeField] private bool isInLove;

    [SerializeField] private float followHisLoveSpeed;

    [SerializeField] private float giveLoveSpeed;

    [SerializeField] private Material loverMaterial;

    [SerializeField] private Material noneLoverMaterial;

    private List<LoverMode> closeLovers = new List<LoverMode>();

    private LoverMode targetLover;

    private Renderer loverRenderer;

    private float lastDirection;

    void Start()
    {
        loverRenderer = GetComponentInParent<Renderer>();
        lastDirection = Random.Range(0, MathF.PI * 5);
    }

    void Update()
    {
        FindLover();
        MoveNormally();
        FollowHisLove();
        GiveLoveToTarget();
        UpdateLoverApparence();
    }


    private void MoveNormally()
    {
        if (targetLover is null)
        {
            lastDirection = Random.Range(lastDirection - 0.15f, lastDirection + 0.15f);

            var newPos = charactertransform.position + new Vector3(
                Mathf.Sin(lastDirection),
                0,
                Mathf.Cos(lastDirection)
            );

            charactertransform.position = Vector3.Lerp(charactertransform.position, newPos, followHisLoveSpeed * Time.deltaTime);
        }
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
        isInLove = true;
        GameManager.Instance.IncrementGameCounter(1);
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
                GameManager.Instance.IncrementGameCounter(1);
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
