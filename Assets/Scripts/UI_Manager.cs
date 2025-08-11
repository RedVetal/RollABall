using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public Button exitButton;  // Кнопка "Выйти" из Minigame
    public GameObject winPanel;  // Панель победы (опционально)
    public GameObject losePanel; // Панель поражения (опционально)

    private void Start()
    {
        // Настраиваем кнопку "Выйти"
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitToMenu);
        }
        else
        {
            Debug.LogError("Exit Button not assigned in UIManager!");
        }
    }

    // Вызывается при победе/поражении (если нужно управлять UI отсюда)
    public void ShowWinPanel() => winPanel?.SetActive(true);
    public void ShowLosePanel() => losePanel?.SetActive(true);

    // Метод для кнопки "Выйти"
    private void ExitToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ExitToMenu();
        }
        else
        {
            Debug.LogWarning("GameManager not found! Loading menu directly.");
            SceneManager.LoadScene("Menu");
        }
    }
}