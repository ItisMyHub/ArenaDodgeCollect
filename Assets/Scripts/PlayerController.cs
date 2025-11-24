using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls player movement using the Unity Input System.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody playerRb;
    private Vector2 moveInput;                   // Input from WASD / stick
    private InputSystem_Actions inputActions;    // Generated input actions class

    private void Awake()
    {
        // Cache the Rigidbody component
        playerRb = GetComponent<Rigidbody>();

        // Create and configure the input actions instance
        inputActions = new InputSystem_Actions();

        // Subscribe to Move action events
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled  += OnMoveCanceled;
    }

    private void OnEnable()
    {
        // Enable all input actions when this object becomes active
        inputActions.Enable();
    }

    private void OnDisable()
    {
        // Disable input when this object is disabled
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled  -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read movement input as a 2D vector (x = horizontal, y = vertical)
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset input to zero when movement stops
        moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // Convert 2D input to a 3D direction (on the XZ plane)
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveVelocity = moveDir * moveSpeed;

        // Apply movement using the Rigidbody's linear velocity
        // (Y component is kept from physics, e.g. gravity)
        playerRb.linearVelocity = new Vector3(
            moveVelocity.x,
            playerRb.linearVelocity.y,
            moveVelocity.z
        );
    }
}