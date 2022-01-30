using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance { get; private set; }
    [SerializeField] TMP_Text GameEndingText;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameOver += () => GameFinish("Game Over");
        GameManager.Instance.OnGameSuccess += () => GameFinish("Congratulation!");
        GameManager.Instance.OnGameStart += GameStart;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        this.enabled = false;
    }

    public void GameFinish(string message)
    {
        this.enabled = true;
        GameEndingText.text = message;
    }
}
