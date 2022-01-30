using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCounter : MonoBehaviour
{
    [SerializeField] float EndCounter = 90f;
    [SerializeField] Slider ProgressBar;
    [SerializeField] TMP_Text Percentage;

    private float _currentCount = 0f;

    void Start()
    {
        GameManager.Instance.OnGameCounterIncrement += AdjustCounter;
        ProgressBar.value = 0;
        Percentage.text = "0%";
    }

    void AdjustCounter(int inc)
    {
        if (EndCounter == 0) return;

        _currentCount += inc;
        ProgressBar.value = _currentCount / EndCounter;

        Percentage.text = $"{Mathf.Round(ProgressBar.value * 100)}%";

        if (_currentCount >= EndCounter) GameManager.Instance.Success();
    }
}
