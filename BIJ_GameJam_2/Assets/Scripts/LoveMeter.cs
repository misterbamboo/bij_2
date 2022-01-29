using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveMeter : MonoBehaviour
{
    [SerializeField]
    private int maxLove = 100;

    private int currentLove;

    public event Action<float> OnLovePctChanged = delegate { };

    void OnEnable()
    {
        currentLove = 0;
        OnLovePctChanged(GetCurrentLovePct());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModifyLove(10);
        }
    }

    public void ModifyLove(int amount)
    {
        int newLoveAmount = currentLove + amount;
        if (newLoveAmount <= maxLove)
        {
            currentLove += amount;
            OnLovePctChanged(GetCurrentLovePct());
        }
    }

    private float GetCurrentLovePct()
    { 
        return (float)currentLove / (float)maxLove;
    }
}
