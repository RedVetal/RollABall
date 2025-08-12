using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        // ѕроверка и инициализаци€
        if (exitButton == null)
            Debug.LogError("ExitButton не назначен в инспекторе!", this);
        else
            exitButton.onClick.AddListener(ReturnToMenu);

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void UpdateScore(int current, int total)
    {
        if (countText != null)
            countText.text = $"Count: {current}/{total}";
    }

    public void ShowEndGamePanel(bool isWin)
    {
        if (winPanel != null) winPanel.SetActive(isWin);
        if (losePanel != null) losePanel.SetActive(!isWin);
        GameManager.Instance?.TogglePause(true);
    }

    public void ReturnToMenu()
    {
        GameManager.Instance?.TogglePause(false);
        GameManager.Instance?.LoadScene("Main");
    }

    // ƒобавленный метод дл€ доступа к countText
    public TextMeshProUGUI GetCountTextComponent() => countText;
}