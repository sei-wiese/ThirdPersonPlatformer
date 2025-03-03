using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10f;         // Default movement speed
    [SerializeField] private Transform cameraTransform;      // Reference to the camera's Transform
    [SerializeField] private InputManager inputManager;      // Reference to the InputManager

    [SerializeField] private float jumpForce = 7f;           // jump force
    [SerializeField] private int maxJumps = 2;               // Number of jumps allowed
    
    [SerializeField] private float dashSpeedMultiplier = 3f;   // Speed multiplier when Dash

    private Rigidbody rb;
    private bool isGrounded = false;
    private int jumpCount = 0;
    private bool isDashing = false;


    void Start() {
        rb = GetComponent<Rigidbody>();

        // Register listeners for each event from the InputManager
        if (inputManager != null) {
            inputManager.OnMove.AddListener(MovePlayer);
            inputManager.OnJump.AddListener(Jump);
            inputManager.OnDash.AddListener(Dash);
        }
    }

        void Update() {
        // Update the dash flag based on whether the Left Shift key is being held down
        if (!Input.GetKey(KeyCode.LeftShift)) {
            isDashing = false;
        }
    }



    // Movement based on WASD input (currently no speed multiplier for holding Shift)
    private void MovePlayer(Vector2 input) {
        if (input.sqrMagnitude < 0.01f) return;

        // Get the camera's forward and right vectors (zero out the Y component, then normalize)
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Calculate movement direction based on the input
        Vector3 moveDir = (camForward * input.y) + (camRight * input.x);
        moveDir.Normalize();

        // Dash が有効なら、速度倍率を適用
        float speedMultiplier = isDashing ? dashSpeedMultiplier : 1f;

        // Move using the Rigidbody
        // rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + moveDir * moveSpeed * speedMultiplier * Time.deltaTime);

        // Rotate the character to face the movement direction
        transform.forward = moveDir;
    }


    // Jump logic (only allowed when on the ground)
    private void Jump() {
        if (isGrounded || jumpCount < maxJumps) {
            // Reset the current Y-axis velocity before applying upward force
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }
    
    // Dash logic: activated when the OnDash event is called from the InputManager
    private void Dash() {
        isDashing = true;
    }

    // Ground detection: checks collision with any object tagged as "Ground"
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when touching the ground
        }
    }

    // When the player is no longer colliding with the ground, mark isGrounded as false
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }

}
