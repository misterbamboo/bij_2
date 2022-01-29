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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        GameEndingText.text = "Game Over";
    }
}
