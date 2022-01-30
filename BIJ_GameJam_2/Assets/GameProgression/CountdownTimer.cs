using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance { get; private set; }

    [SerializeField] TMP_Text TimerText;

    [SerializeField] public float StartTime;

    private bool _stopTimer = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.Instance.OnGameOver += GameFinish;
        GameManager.Instance.OnGameSuccess += GameFinish;
        GameManager.Instance.OnGameStart += GameStart;
    }

    void Update()
    {
        if (_stopTimer) return;

        StartTime -= Time.deltaTime;
        var time = TimeSpan.FromSeconds(StartTime);

        if (time <= TimeSpan.Zero)
        {
            GameManager.Instance.TimeOut();
        }

        TimerText.text = $"{time:mm\\:ss\\:fff}";
    }

    public void GameStart()
    {
        this.enabled = true;
        this.TimerText.enabled = true;
        this._stopTimer = false;
    }

    public void GameFinish()
    {
        this.enabled = false;
        this.TimerText.enabled = false;
        this._stopTimer = true;
    }
}