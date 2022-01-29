using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveMeter : MonoBehaviour
{
    [SerializeField]
    private float maxLove = 100.0f;

    private float currentLove;

    public event Action<float> OnLovePctChanged = delegate { };

    void OnEnable()
    {
        currentLove = 0;
        OnLovePctChanged(GetCurrentLovePct());
    }
    
    public void ModifyLove(float amount)
    {
        var newLoveAmount = currentLove + amount;
        if (newLoveAmount <= maxLove)
        {
            currentLove += amount;
            OnLovePctChanged(GetCurrentLovePct());
        }
    }

    private float GetCurrentLovePct()
    { 
        return currentLove / maxLove;
    }
}
