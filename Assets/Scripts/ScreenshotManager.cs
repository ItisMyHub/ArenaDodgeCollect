using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

/// <summary>
/// Allows taking screenshots with a key press (F12) using the new Input System.
/// Saves PNG files in the persistent data path.
/// </summary>
public class ScreenshotManager : MonoBehaviour
{
    private InputAction screenshotAction;
    private int screenshotIndex = 0;

    private void OnEnable()
    {
        // Bind F12 using the new Input System
        screenshotAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Keyboard>/f12"
        );
        screenshotAction.performed += OnScreenshot;
        screenshotAction.Enable();
    }

    private void OnDisable()
    {
        if (screenshotAction != null)
        {
            screenshotAction.performed -= OnScreenshot;
            screenshotAction.Disable();
        }
    }

    private void OnScreenshot(InputAction.CallbackContext context)
    {
        TakeScreenshot();
    }

    private void TakeScreenshot()
    {
        string folder = Application.persistentDataPath;
        string fileName = $"screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}_{screenshotIndex}.png";
        string fullPath = Path.Combine(folder, fileName);

        ScreenCapture.CaptureScreenshot(fullPath);
        screenshotIndex++;

        Debug.Log($"[ScreenshotManager] Saved screenshot: {fullPath}");
    }
}