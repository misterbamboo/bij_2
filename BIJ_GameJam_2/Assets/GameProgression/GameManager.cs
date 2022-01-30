using Assets.GameProgression;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MapBoundries MapBoundries => mapBoundries;
    [SerializeField] private MapBoundries mapBoundries;

    [SerializeField] float TimeBeforeRestart = 10;

    [SerializeField] private Vector2 mapSize;
    public float MapSizeX => mapSize.x;
    public float MapSizeZ => mapSize.y;

    public event Action OnGameOver = delegate { };
    public event Action OnGameSuccess = delegate { };
    public event Action OnGameStart = delegate { };
    public event Action<int> OnGameCounterIncrement = delegate { };

    public event Action<GameEvents> OnGameEvent;

    void Awake()
    {
        Instance = this;
    }

    public void GameEvent(GameEvents gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
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
        SceneManager.LoadScene(0);
    }
}
