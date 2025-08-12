using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);

        // ¬осстанавливаем нормальную скорость времени
        Time.timeScale = 1f;
    }

    private void StartGame() => GameManager.Instance?.LoadScene("Minigame");
    private void QuitGame() => GameManager.Instance?.QuitGame();
}