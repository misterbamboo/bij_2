using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCounter : MonoBehaviour
{
    public static GameCounter Instance { get; private set; }

    [SerializeField] Slider ProgressBar;
    [SerializeField] TMP_Text Percentage;

    private float EndCounter = 90f;
    private float _currentCount = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.Instance.OnGameCounterChanged += AdjustCounter;
        GameManager.Instance.OnGameOver += StopCounter;
        ProgressBar.value = 0;
        Percentage.text = "0%";
    }

    public void SetCounter(float counter)
    {
        EndCounter = counter;
    }

    void StopCounter()
    {
        GameManager.Instance.OnGameCounterChanged -= AdjustCounter;
    }

    void AdjustCounter(int amount)
    {
        if (EndCounter == 0) return;

        _currentCount = amount;
        ProgressBar.value = _currentCount / EndCounter;

        Percentage.text = $"{Mathf.Floor(ProgressBar.value * 100)}%";

        if (_currentCount >= EndCounter) GameManager.Instance.Success();
    }
}
