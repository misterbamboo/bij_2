using Assets.GameProgression;
using System;
using System.Collections;
using System.Linq;
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
    public event Action<int> OnGameCounterChanged = delegate { };

    public event Action<GameEvents> OnGameEvent;

    void Awake()
    {
        Instance = this;
    }

    public void GameEvent(GameEvents gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }

    public void GameCounterChanged()
    {
        Lover[] lovers = GameObject.FindObjectsOfType<Lover>();
        OnGameCounterChanged(lovers.Count(x => x.IsInLove));
    }

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
