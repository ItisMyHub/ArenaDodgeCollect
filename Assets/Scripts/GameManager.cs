using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Playing;
    public SceneLoader SceneLoader {get; set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log($"[GameManager] Initialized. State = {CurrentState}");
    }

    private void Start()
    {
    // Try to find a SceneLoader in the scene if none is set yet
        if (SceneLoader == null)
        {
            SceneLoader = FindAnyObjectByType<SceneLoader>();
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] State changed to: {CurrentState}");
    }

    
    public void GameOver()
    {
        SetState(GameState.GameOver);

        if (SceneLoader != null)
    {
            SceneLoader.LoadResult();
    }
        else
    {
            Debug.LogWarning("[GameManager] GameOver called but SceneLoader is not set.");
    }
    }
}