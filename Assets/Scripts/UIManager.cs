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

    public TextMeshProUGUI GetCountText() => countText; // Новый метод

    private void Awake()
    {
        InitializeUI();
        ValidateReferences();
    }

    private void InitializeUI()
    {
        winPanel?.SetActive(false);
        losePanel?.SetActive(false);

        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitToMenu);
        }
    }

    private void ValidateReferences()
    {
        Debug.Assert(exitButton != null, "ExitButton не назначен!", this);
        Debug.Assert(winPanel != null, "WinPanel не найден!", this);
        Debug.Assert(losePanel != null, "LosePanel не найден!", this);
        Debug.Assert(countText != null, "CountText не найден!", this);
    }

    public void ShowEndGamePanel(bool isWin)
    {
        if (winPanel != null) winPanel.SetActive(isWin);
        if (losePanel != null) losePanel.SetActive(!isWin);
    }

    private void ExitToMenu() => GameManager.Instance?.LoadScene("Menu");
}