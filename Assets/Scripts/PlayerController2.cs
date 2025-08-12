using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController2 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;

    // Константы
    private const string EnemyTag = "Enemy";
    private const string PickupTag = "Pickup";
    private const string CountTextName = "CountText";

    // Компоненты
    private Rigidbody rb;
    private TextMeshProUGUI countText;
    private GameObject enemy;
    private UIManager uiManager;

    // Игровые переменные
    private int count;
    private bool isGameOver;
    private float movementX, movementY;

    private void Awake()
    {
        // Основные компоненты
        rb = GetComponent<Rigidbody>();
        enemy = GameObject.FindWithTag(EnemyTag);
        uiManager = FindAnyObjectByType<UIManager>();

        // Получаем countText через UIManager, если он найден
        if (uiManager != null)
        {
            countText = uiManager.GetCountTextComponent();
        }

        // Резервный поиск, если через UIManager не найден
        if (countText == null)
        {
            GameObject textObj = GameObject.Find(CountTextName);
            if (textObj != null)
                countText = textObj.GetComponent<TextMeshProUGUI>();
        }

        // Валидация
        if (countText == null)
            Debug.LogWarning($"[PlayerController2] Не найден TextMeshProUGUI для счётчика {CountTextName}.", this);

        if (uiManager == null)
            Debug.LogWarning("[PlayerController2] UIManager не найден в сцене!", this);
    }

    private void Start()
    {
        ResetGameState();
    }

    private void ResetGameState()
    {
        count = 0;
        isGameOver = false;
        UpdateCountText();
    }

    private void OnMove(InputValue movementValue)
    {
        if (isGameOver) return;
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            Vector3 movement = new Vector3(movementX, 0f, movementY);
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver && other.CompareTag(PickupTag))
        {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject.CompareTag(EnemyTag))
        {
            EndGame(false);
        }
    }

    private void UpdateCountText()
    {
        if (countText == null) return;

        if (uiManager != null && GameManager.Instance != null)
        {
            uiManager.UpdateScore(count, GameManager.Instance.totalPickups);
        }
        else
        {
            // Фоллбэк: меняем текст вручную
            countText.text = $"Count: {count}";
        }
    }

    private void EndGame(bool isWin)
    {
        isGameOver = true;
        rb.linearVelocity = Vector3.zero;

        if (isWin && enemy != null)
            Destroy(enemy);

        uiManager?.ShowEndGamePanel(isWin);
    }
}
