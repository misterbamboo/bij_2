using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameCounter : MonoBehaviour
{
    [SerializeField] int EndCounter = 90;
    [SerializeField] TMP_Text CounterText;

    private int _currentCount = 0;

    void Start()
    {
        GameManager.Instance.OnGameCounterIncrement += AdjustCounter;
        CounterText.text = $"0%";
    }

    void AdjustCounter(int inc)
    {
        if (EndCounter == 0) return;

        _currentCount += inc;
        CounterText.text = $"{Mathf.Round(_currentCount / EndCounter * 100)}%";

        if (_currentCount >= EndCounter) GameManager.Instance.Success();
    }
}
