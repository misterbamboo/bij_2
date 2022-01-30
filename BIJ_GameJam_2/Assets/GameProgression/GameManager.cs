using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] float TimeBeforeRestart = 10;

    public event Action OnGameOver = delegate { };
    public event Action OnGameSuccess = delegate { };
    public event Action OnGameStart = delegate { };
    public event Action<int> OnGameCounterIncrement = delegate { };

    void Awake()
    {
        Instance = this;
    }

    public void IncrementGameCounter(int by) => OnGameCounterIncrement(by);

    public void Success()
    {
        OnGameSuccess();
    }

    public void TimeOut()
    {
        OnGameOver();
        StartCoroutine(AutoRestartGame());
    }

    IEnumerator AutoRestartGame()
    {
        yield return new WaitForSeconds(TimeBeforeRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
