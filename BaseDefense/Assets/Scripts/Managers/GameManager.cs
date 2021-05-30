using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }
    private GameState _currentGameState = GameState.PREGAME;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    //Persistent managers.
    public GameObject[] SystemPrefabs;
    List<GameObject> _instancedSystemPrefabs = new List<GameObject>();

    //Load operations
    List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    string _currentLevelName = string.Empty;


    public event Action<GameManager.GameState, GameManager.GameState> OnGameStateChanged;

    private void Start()
    {
        InstantiateSystemPrefabs();
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (_currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void UpdateState(GameState gameState)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = gameState;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(CurrentGameState, previousGameState);
    }

    #region Basic game operations
    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void RestartGame()
    {
        UnloadLevel("Main");
    }
    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.PAUSED ? GameState.RUNNING : GameState.PAUSED);
    }

    public void ExitGame()
    {
        UnloadLevel("Main");
    }

    #endregion

    #region Generating Persistent Managers;
    private void InstantiateSystemPrefabs()
    {
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            _instancedSystemPrefabs.Add(Instantiate(SystemPrefabs[i]));
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    #endregion

    #region loading and unloading scenes

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Could not load scene " + levelName);
            return;
        }
        _currentLevelName = levelName;
        _loadOperations.Add(ao);
        ao.completed += OnLoadLevelComplete;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Could not unload scene: " + levelName);
            return;
        }
        ao.completed += OnUnloadLevelComplete;
    }

    private void OnLoadLevelComplete(AsyncOperation ao)
    {
        

        if (_loadOperations.Remove(ao))
        {
            if (_loadOperations.Count == 0)
            {
                Scene scene = SceneManager.GetSceneByName(_currentLevelName);
                SceneManager.SetActiveScene(scene);

                UpdateState(GameState.RUNNING);
            }
        }
    }

    private void OnUnloadLevelComplete(AsyncOperation ao)
    {
        UpdateState(GameState.PREGAME);
    }

    #endregion
}
