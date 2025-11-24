using UnityEngine;
using UnityEngine.InputSystem; // New Input System namespace

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody playerRb;
    private Vector2 moveInput; // from Input System
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();

        // Create and configure the input actions instance
        inputActions = new InputSystem_Actions();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        // Clean up event subscriptions (good practice)
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // Convert 2D input (x = horizontal, y = vertical) to 3D direction
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveVelocity = moveDir * moveSpeed;

        playerRb.linearVelocity = new Vector3(moveVelocity.x, playerRb.linearVelocity.y, moveVelocity.z);
    }
}