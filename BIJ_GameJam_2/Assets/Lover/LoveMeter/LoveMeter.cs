using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveMeter : MonoBehaviour
{
    public bool IsFull => currentLove >= maxLove;

    [SerializeField]
    private LoverMode loverMode;

    [SerializeField]
    private float maxLove = 100;

    private float currentLove;


    public event Action<float> OnLovePctChanged = delegate { };

    void OnEnable()
    {
        currentLove = 0;
    }

    public void ModifyLove(float amount)
    {
        currentLove = Math.Clamp(currentLove + amount, 0, maxLove);

        if (IsFull)
        {
            loverMode.PutInLove();
        }

        OnLovePctChanged(GetCurrentLovePct());
    }

    private float GetCurrentLovePct()
    {
        return (float)currentLove / (float)maxLove;
    }
}
