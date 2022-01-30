using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.GameProgression
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject creditPanel;

        [SerializeField]
        TextMeshProUGUI textButton;

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ShowOrHideCredit()
        {
            creditPanel.SetActive(!creditPanel.activeSelf);

            if (creditPanel.activeSelf)
            {
                textButton.text = "Close";
            }
            else
            { 
                textButton.text = "Credits";
            }
        }
    }
}
