using UnityEngine;
using UnityEngine.SceneManagement;

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
        // This will only quit a built game, not the editor
        Application.Quit();
    }
}