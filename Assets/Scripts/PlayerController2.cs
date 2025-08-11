using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5;

    [Header("UI References")]
    public TextMeshProUGUI countText;
    public GameObject winPanel;
    public GameObject losePanel;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;

    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        UpdateCountText();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseGame();
        }
    }

    private void UpdateCountText()
    {
        countText.text = $"Count: {count}/{GameManager.Instance.totalPickups}";

        if (count >= GameManager.Instance.totalPickups)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        isGameOver = true;
        rb.linearVelocity = Vector3.zero;
        winPanel.SetActive(true);
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    private void LoseGame()
    {
        isGameOver = true;
        rb.linearVelocity = Vector3.zero;
        losePanel.SetActive(true);
    }

    // ������ "�����" �� �������� UI
    //public void ExitToMenu()
    //{
    //    if (GameManager.Instance != null)
    //    {
    //        GameManager.Instance.LoadMenu();
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("Menu");
    //    }
    //}
}
