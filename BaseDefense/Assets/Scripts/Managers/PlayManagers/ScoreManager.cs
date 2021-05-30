using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int _baseMaxHealth = 1000;
    private int _baseHealth;
    private int _currentScore = 0;

    public event Action<int> OnScoreUpdate;
    public event Action OnGameOver;

    private void Start()
    {
        GamePhaseManager.Instance.OnGamePhaseChanged += HandleGamePhaseChanged;
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameManager.GameState newState, GameManager.GameState previousState)
    {
        if (newState == GameManager.GameState.PREGAME)
        {
            HighscoreManager.Instance.UpdateHighScore(_currentScore);
        }
    }

    #region Game phase

    private void HandleGamePhaseChanged(GamePhaseManager.GamePhase gamePhase)
    {
        switch (gamePhase)
        {
            case GamePhaseManager.GamePhase.STARTING:
                HandleGameStart();
                break;
            case GamePhaseManager.GamePhase.PLAYING:
                
                break;
            case GamePhaseManager.GamePhase.GAMEOVER:
                HandleGameOver();
                break;
            default:
                break;
        }
    }

    private void HandleGameStart()
    {
        _currentScore = 0;
        _baseHealth = _baseMaxHealth;

        EnemyUnit.OnUnitDiedUpdateScore += UpdateScore;
        HandleOutOfBounds.OnDamageToBase += UpdateBaseHealth;
    }

    private void HandleGameOver()
    {
        EnemyUnit.OnUnitDiedUpdateScore -= UpdateScore;
        HandleOutOfBounds.OnDamageToBase -= UpdateBaseHealth;

        HighscoreManager.Instance.UpdateHighScore(_currentScore);
    }

    #endregion

    #region Score

    private void UpdateScore(int _scoreToAdd)
    {
        _currentScore += _scoreToAdd;

        OnScoreUpdate?.Invoke(_currentScore);
    }

    private void UpdateBaseHealth(int _healthLost)
    {
        _baseHealth -= _healthLost;
        if (_baseHealth <= 0)
        {
            
            OnGameOver?.Invoke();
        }
    }
    #endregion
}
