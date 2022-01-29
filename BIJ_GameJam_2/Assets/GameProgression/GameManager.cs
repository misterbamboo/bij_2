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
    public event Action OnGameStart = delegate { };

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
