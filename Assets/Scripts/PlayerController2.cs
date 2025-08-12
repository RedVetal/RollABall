using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController2 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    // Компоненты
    private Rigidbody rb;
    private TextMeshProUGUI countText;
    private GameObject enemy;
    private UIManager uiManager;

    // Игровые переменные
    private float movementX, movementY;
    private int count;
    private bool isGameOver;

    // Константы
    private const string CountTextName = "CountText";
    private const string EnemyTag = "Enemy";
    private const string PickupTag = "Pickup";

    private void Awake()
    {
        InitializeComponents();
        ValidateComponents();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GameObject.FindWithTag(EnemyTag);
        uiManager = FindAnyObjectByType<UIManager>(); // Исправлено на современный метод

        // Поиск CountText через UIManager
        if (uiManager != null)
        {
            countText = uiManager.GetCountText(); // Добавьте этот метод в UIManager
        }

        // Резервный поиск
        if (countText == null)
        {
            GameObject textObj = GameObject.Find(CountTextName);
            if (textObj != null) countText = textObj.GetComponent<TextMeshProUGUI>();
        }
    }

    private void ValidateComponents()
    {
        if (countText == null)
            Debug.LogError($"CountText ({CountTextName}) не найден!", this);

        if (uiManager == null)
            Debug.LogError("UIManager не найден в сцене!", this);
    }

    private void Start() => ResetGameState();

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
            rb.AddForce(new Vector3(movementX, 0f, movementY) * speed);
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
            EndGame(false);
    }

    private void UpdateCountText()
    {
        if (countText != null && GameManager.Instance != null)
            countText.text = $"Count: {count}/{GameManager.Instance.totalPickups}";
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