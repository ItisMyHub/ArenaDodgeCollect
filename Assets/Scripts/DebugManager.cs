using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

/// <summary>
/// Simple debug mode: toggles overlay + god mode with F1 (new Input System)
/// and shows FPS and status in a UI text.
/// </summary>
public class DebugManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText; // or UnityEngine.UI.Text
    [SerializeField] private bool godMode = false;      // starts off by default

    private bool debugVisible = false;
    private float deltaTime = 0f;

    private InputAction toggleDebugAction;

    private void OnEnable()
    {
        // Bind F1 using the new Input System
        toggleDebugAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/f1");
        toggleDebugAction.performed += OnToggleDebug;
        toggleDebugAction.Enable();

        if (debugText != null)
        {
            debugText.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (toggleDebugAction != null)
        {
            toggleDebugAction.performed -= OnToggleDebug;
            toggleDebugAction.Disable();
        }
    }

    private void OnToggleDebug(InputAction.CallbackContext context)
    {
        // Toggle both visibility and god mode
        debugVisible = !debugVisible;
        godMode = !godMode;

        if (debugText != null)
        {
            debugText.gameObject.SetActive(debugVisible);
        }

        Debug.Log($"[DebugManager] Debug mode toggled. GodMode: {godMode}");
    }

    private void Update()
    {
        // FPS calculation
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        if (debugVisible && debugText != null)
        {
            debugText.text = $"DEBUG MODE\nFPS: {fps:0.}\nGodMode: {godMode}";
        }
    }

    public bool IsGodMode => godMode;
}