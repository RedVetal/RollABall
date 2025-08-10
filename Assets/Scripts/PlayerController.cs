using UnityEngine;
using UnityEngine.InputSystem;  // Один код для клавиатуры, геймпада, мобильных касаний.
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves.
    public float speed = 0;
    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;
    // UI object to display winning text.
    public GameObject winTextObject;

    // Rigidbody of the player.
    private Rigidbody rb;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        // Initialize count to zero.
        count = 0;
        // Update the count display.
        SetCountText();
        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();
        // Check if the count has reached or exceeded the win condition.
        if (count > 10)
        {
            // Display the win text.
            winTextObject.SetActive(true);
        }
    }



    // Input System по умолчанию для джойстиков/клавиш WASD возвращает Vector2 (только X и Y)
    // This function is called when a move input is detected.
    private void OnMove(InputValue movementValue)   // OnMove (Input System) Это событие, вызываемое при изменении ввода(даже между кадрами).
                                                    // Оно просто сохраняет последние данные ввода в movementX/Y.

    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate: Берёт сохранённые значения и применяет силу к Rigidbody.
    // Это гарантирует, что физика будет обрабатывать движение в своём темпе.
    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()  // Используйте FixedUpdate только для физики (Rigidbody).
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Y становится Z

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            // Increment the count of "PickUp" objects collected.
            count++;
            // Update the count display.
            SetCountText();
        }
    }
}



//////////////////////////////////////////////////////////////////////////////
///контроллер без Input System, используя классическую систему ввода Unity.
///

//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float jumpForce = 5f;
//    [SerializeField] private GameObject projectilePrefab;

//    private Rigidbody rb;
//    private bool isGrounded;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {
//        // Движение
//        float horizontal = Input.GetAxis("Horizontal");
//        float vertical = Input.GetAxis("Vertical");

//        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
//        transform.Translate(movement);

//        // Прыжок
//        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//        {
//            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//            isGrounded = false;
//        }

//        // Выстрел
//        if (Input.GetKeyDown(KeyCode.Mouse0))
//        {
//            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
//        }
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//        }
//    }
//}
