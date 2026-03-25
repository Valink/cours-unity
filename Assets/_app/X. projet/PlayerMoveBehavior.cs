using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float airControlMultiplier = 0.5f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("Input")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();

        jumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();

        jumpAction.action.performed -= OnJump;
    }

    private void Update()
    {
        // Read movement input every frame
        moveInput = moveAction.action.ReadValue<Vector2>();

        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        // Convert to world space relative to player orientation
        move = transform.TransformDirection(move);

        float control = isGrounded ? 1f : airControlMultiplier;

        Vector3 targetVelocity = move * moveSpeed * control;

        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange = new Vector3(
            targetVelocity.x - velocity.x,
            0f,
            targetVelocity.z - velocity.z
        );

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // Handle jump
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = true;
    }
}
