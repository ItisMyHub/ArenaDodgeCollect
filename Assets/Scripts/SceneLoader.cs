using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple helper to load different scenes from UI buttons.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public void LoadArena()
    {
        SceneManager.LoadScene("Arena");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadResult()
    {
        SceneManager.LoadScene("Result");
    }

    public void QuitGame()
    {
        // Only quits the built game, has no effect in the editor
        Application.Quit();
    }
}