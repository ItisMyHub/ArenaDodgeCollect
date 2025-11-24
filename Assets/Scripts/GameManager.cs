using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float ElapsedTime { get; private set; }
    private bool _isTiming;

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

   private void OnEnable()
    {
        // Make sure we always refresh the SceneLoader when scenes change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        if (scene.name == "Arena")
        {
            ElapsedTime = 0f;
            SetState(GameState.Playing);
        }
        else
        {
            SceneLoader = null;
            Debug.LogWarning($"[GameManager] No SceneLoader found in scene: {scene.name}");
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] State changed to: {CurrentState}");

        _isTiming = (CurrentState == GameState.Playing);
    }

    private void Update()
    {
        if (_isTiming)
        {
            ElapsedTime += Time.deltaTime;
        }
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