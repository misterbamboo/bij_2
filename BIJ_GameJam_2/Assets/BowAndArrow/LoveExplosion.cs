using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveExplosion : MonoBehaviour
{
    [SerializeField]
    private float AmountOfLove = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Lover"))
        {
            return;
        }

        GameManager.Instance.GameEvent(Assets.GameProgression.GameEvents.LoverHitByArrow);

        var loverMeter = other.GetComponentInChildren<LoveMeter>();
        loverMeter?.ModifyLove(AmountOfLove);
    }
}
