using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance { get; private set; }

    [SerializeField] TMP_Text TimerText;

    [SerializeField] float StartTime;

    private bool _stopTimer = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        if (_stopTimer) return;

        StartTime -= Time.deltaTime;
        var time = TimeSpan.FromSeconds(StartTime);

        if (time <= TimeSpan.Zero)
        {
            EndTime();
        }

        TimerText.text = $"{time:mm\\:ss}";
    }

    private void EndTime()
    {
        _stopTimer = true;
        EndGame.Instance.GameOver();
    }
}