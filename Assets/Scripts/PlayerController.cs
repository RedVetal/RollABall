using UnityEngine;
using UnityEngine.InputSystem;  // Один код для клавиатуры, геймпада, мобильных касаний.

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Y становится Z

        // Apply force to the Rigidbody to move the player.
        rb.AddForce (movement * speed);
    }


    // Input System по умолчанию для джойстиков/клавиш WASD возвращает Vector2 (только X и Y)
    // This function is called when a move input is detected.
    private void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
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
