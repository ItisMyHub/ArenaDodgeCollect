using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Central game controller that persists between scenes.
/// Manages game state, time survived, and score.
/// </summary>
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

    public float ElapsedTime { get; private set; }   // Time spent in the current run
    public int Score { get; private set; }           // Collected score in current run

    // Reference to the SceneLoader in the currently loaded scene
    public SceneLoader SceneLoader { get; set; }

    private bool _isTiming;

    private void Awake()
    {
        // Singleton pattern: keep only one GameManager alive
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log($"[GameManager] Initialized. State = {CurrentState}");
    }

    private void OnEnable()
    {
        // Listen for scene load events so we can refresh SceneLoader and reset run data
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // Track how long the player has been alive in this run
        if (_isTiming)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find a SceneLoader in the newly loaded scene
        var loader = FindAnyObjectByType<SceneLoader>();
        if (loader != null)
        {
            SceneLoader = loader;
            Debug.Log($"[GameManager] SceneLoader set for scene: {scene.name}");
        }
        else
        {
            SceneLoader = null;
            Debug.LogWarning($"[GameManager] No SceneLoader found in scene: {scene.name}");
        }

        // When we enter the Arena, start a new run
        if (scene.name == "Arena")
        {
            ElapsedTime = 0f;
            Score = 0;
            SetState(GameState.Playing);
        }
    }

    /// <summary>
    /// Change the current game state and start/stop timing.
    /// </summary>
    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] State changed to: {CurrentState}");

        _isTiming = (CurrentState == GameState.Playing);
    }

    /// <summary>
    /// Add to the player's score for the current run.
    /// </summary>
    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log($"[GameManager] Score increased to: {Score}");
    }

    /// <summary>
    /// Called when the player hits a hazard and the run ends.
    /// </summary>
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